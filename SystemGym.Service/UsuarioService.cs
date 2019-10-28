using Microsoft.EntityFrameworkCore;
using Montreal.Process.Sistel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Pessoa;
using SystemGym.Model.Usuario;

namespace SystemGym.Service
{
    public class UsuarioService
    {
        private readonly SystemGymContext context;
        private readonly PessoaService pessoaService;
        public UsuarioService(SystemGymContext context, PessoaService pessoaService)
        {
            this.context = context;
            this.pessoaService = pessoaService;
        }

        public List<UsuarioReturnModel> GetAll()
        {
            return this.context.Usuario
                .Include(x => x.Pessoa)
                .OrderBy(x => x.Pessoa.Nome)
                .ToList()
                .Select(x => new UsuarioReturnModel()
                {
                    UsuarioId = x.UsuarioId,
                    UserName = x.UserName,
                    Password = x.Password,
                    DataCadastro = x.DataCadastro,
                    DataAlteracao = x.DataAlteracao,
                    DataAcesso = x.DataAcesso,
                    Pessoa = new PessoaReturnModel()
                    {
                        PessoaId = x.Pessoa.PessoaId,
                        Nome = x.Pessoa.Nome,
                        Cpf = x.Pessoa.Cpf,
                        Email = x.Pessoa.Email,
                        TelefoneCasa = x.Pessoa.TelefoneCasa,
                        TelefoneCelular = x.Pessoa.TelefoneCelular,
                        Endereco = x.Pessoa.Endereco,
                        SexoId = x.Pessoa.SexoId,
                        TipoId = x.Pessoa.TipoId,
                        AlteracaoData = x.Pessoa.AlteracaoData,
                        CriacaoData = x.Pessoa.CriacaoData,
                    }
                })
                .ToList();
        }
        public UsuarioReturnModel Get(Guid usuarioId)
        {
            return this.context.Usuario
                .Include(x => x.Pessoa)
                .Where(x => x.UsuarioId.Equals(usuarioId))
                .ToList()
                .Select(x => new UsuarioReturnModel()
                {
                    UsuarioId = x.UsuarioId,
                    UserName = x.UserName,
                    Password = x.Password,
                    DataCadastro = x.DataCadastro,
                    DataAlteracao = x.DataAlteracao,
                    DataAcesso = x.DataAcesso,
                    Pessoa = new PessoaReturnModel()
                    {
                        PessoaId = x.Pessoa.PessoaId,
                        Nome = x.Pessoa.Nome,
                        Cpf = x.Pessoa.Cpf,
                        Email = x.Pessoa.Email,
                        TelefoneCasa = x.Pessoa.TelefoneCasa,
                        TelefoneCelular = x.Pessoa.TelefoneCelular,
                        Endereco = x.Pessoa.Endereco,
                        SexoId = x.Pessoa.SexoId,
                        TipoId = x.Pessoa.TipoId,
                        AlteracaoData = x.Pessoa.AlteracaoData,
                        CriacaoData = x.Pessoa.CriacaoData,
                    }
                })
                .FirstOrDefault();
        }

        public async Task<PagingModel<UsuarioReturnModel>> SearchAsync(UsuarioSearchModel usuarioModel)
        {
            var query = this.context.Usuario
                .Include(x => x.Pessoa)
                .AsQueryable();


            if (!string.IsNullOrEmpty(usuarioModel.Nome))
            {
                query = query.Where(x => x.Pessoa.Nome.Equals(usuarioModel.Nome));
            }

            if (!string.IsNullOrEmpty(usuarioModel.Cpf))
            {
                query = query.Where(x => x.Pessoa.Cpf.Equals(usuarioModel.Cpf));
            }

            var pagingModel = new PagingModel<UsuarioReturnModel>();

            pagingModel.TotalItems = query.Count();

            if (!string.IsNullOrEmpty(usuarioModel.Sort))
            {
                query = query.OrderBy(x => x.Pessoa.Nome);
            }

            pagingModel.Items = (await query
                .Skip(usuarioModel.PageSize * (usuarioModel.Page - 1))
                .Take(usuarioModel.PageSize)
                .ToListAsync())
                .Select(x => new UsuarioReturnModel()
                {
                    UsuarioId = x.UsuarioId,
                    Password = x.Password,
                    DataAcesso = x.DataAcesso,
                    UserName = x.UserName,
                    Pessoa = new PessoaReturnModel()
                    {
                        PessoaId = x.PessoaId,
                        Nome = x.Pessoa.Nome,
                        Cpf = x.Pessoa.Cpf,
                        Email = x.Pessoa.Email,
                    }
                }).ToList();

            return pagingModel;
        }

        public UsuarioReturnModel Login(string userName, string password)
        {
            return this.context.Usuario
                .Include(x => x.Pessoa)
                .Where(x => x.UserName.Equals(userName) && x.Password.Equals(password))
                .ToList()
                .Select(x => new UsuarioReturnModel()
                {
                    UsuarioId = x.UsuarioId,
                    Password = x.Password,
                    DataAcesso = x.DataAcesso,
                    Pessoa = new PessoaReturnModel()
                    {
                        PessoaId = x.PessoaId,
                        Nome = x.Pessoa.Nome,
                        Cpf = x.Pessoa.Cpf,
                    }
                })
                .FirstOrDefault();
        }

        public void Adicionar(UsuarioBindingModel usuarioModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {

                try
                {
                    var usuario = new Usuario()
                    {
                        UserName = usuarioModel.UserName,
                        Password = usuarioModel.Password,
                        DataCadastro = DateTime.UtcNow,
                        DataAcesso = DateTime.UtcNow,
                        DataAlteracao = DateTime.UtcNow,
                        PessoaId = pessoaService.Adicionar(usuarioModel.Pessoa)
                    };

                    this.context.Usuario.Add(usuario);
                    this.context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;

                }
            }

        }

        public void Alterar(Guid usuarioId, UsuarioBindingModel usuarioModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var usuario = this.context.Usuario.Where(x => x.UsuarioId.Equals(usuarioId)).FirstOrDefault();

                    usuario.UserName = usuarioModel.UserName;
                    usuario.Password = usuarioModel.Password;
                    usuario.DataAcesso = DateTime.UtcNow;

                    this.pessoaService.Alterar(usuario.PessoaId, usuarioModel.Pessoa);

                    this.context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Delete(Guid usuarioId)
        {
            var usuario = this.context.Usuario
                .Where(x => x.UsuarioId.Equals(usuarioId))
                .FirstOrDefault();

            this.context.Usuario.Remove(usuario);

            this.pessoaService.Delete(usuario.PessoaId);

            this.context.SaveChanges();
        }
    }
}
