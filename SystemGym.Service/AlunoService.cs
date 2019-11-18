using Microsoft.EntityFrameworkCore;
using Montreal.Process.Sistel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Address;
using SystemGym.Model.Aluno;
using SystemGym.Model.Colaborador;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;

namespace SystemGym.Service
{
    public class AlunoService
    {
        private readonly SystemGymContext context;
        private readonly PessoaService pessoaService;
        public AlunoService(SystemGymContext context, PessoaService pessoaService)
        {
            this.context = context;
            this.pessoaService = pessoaService;
        }

        public List<AlunoReturnModel> GetAll()
        {
            return this.context.Aluno
                .Include(x => x.Pessoa)
                .OrderBy(x => x.Pessoa.Nome)
                .ToList().Select(x => new AlunoReturnModel()
                {
                    AlunoId = x.AlunoId,
                    NumeroCartao = x.NumeroCartao,
                    CriacaoData = x.CriacaoData,
                    AlteracaoData = x.AlteracaoData,
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
        public AlunoReturnModel Get(Guid alunoId)
        {
            return this.context.Aluno
                .Include(x => x.Pessoa)
                .Where(x => x.AlunoId.Equals(alunoId))
                .ToList()
                .Select(x => new AlunoReturnModel()
                {
                    AlunoId = x.AlunoId,

                    NumeroCartao = x.NumeroCartao,
                    CriacaoData = x.CriacaoData,
                    AlteracaoData = x.AlteracaoData,
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

        public async Task<PagingModel<AlunoReturnModel>> SearchAsync(AlunoSearchModel alunoModel)
        {
            var query = this.context.Aluno
                .Include(x => x.Pessoa)
                    .ThenInclude(x => x.City)
                .AsQueryable();

            if (!string.IsNullOrEmpty(alunoModel.Nome))
            {
                query = query.Where(x => EF.Functions.Like(x.Pessoa.Nome, "%" + alunoModel.Nome + "%"));
            }

            if (!string.IsNullOrEmpty(alunoModel.Cpf))
            {
                query = query.Where(x => EF.Functions.Like(x.Pessoa.Cpf, "%" + alunoModel.Cpf + "%"));
            }

            var pagingModel = new PagingModel<AlunoReturnModel>();

            pagingModel.TotalItems = query.Count();

            if (!string.IsNullOrEmpty(alunoModel.Sort))
            {
                query = query.OrderBy(x => x.Pessoa.Nome);
            }

            pagingModel.Items = (await query
                .Skip(alunoModel.PageSize * (alunoModel.Page - 1))
                .Take(alunoModel.PageSize)
                .ToListAsync())
                .Select(x => new AlunoReturnModel()
                {
                    AlunoId = x.AlunoId,
                    AlteracaoData = x.AlteracaoData,
                    CriacaoData = x.CriacaoData,
                    NumeroCartao = x.NumeroCartao,
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
                        City = x.Pessoa.City == null ? null : new CityReturnModel()
                        {
                            CityId = x.Pessoa.City.CityId,
                            Name = x.Pessoa.City.Name,
                            StateId = x.Pessoa.City.StateId,
                            State = x.Pessoa.City.State == null ? null : new StateReturnModel()
                            {
                                StateId = x.Pessoa.City.State.StateId,
                                Acronym = x.Pessoa.City.State.Acronym,
                                Name = x.Pessoa.City.State.Name,
                                CountryId = x.Pessoa.City.State.CountryId,
                            }
                        }
                    }
                }).ToList();

            return pagingModel;
        }
        public void Adicionar(AlunoBindingModel alunoModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {

                try
                {
                    var aluno = new Aluno()
                    {
                      NumeroCartao = alunoModel.NumeroCartao,
                         NumeroWhatsapp = alunoModel.NumeroWhatsapp,
                        CriacaoData = DateTime.UtcNow,
                        AlteracaoData = DateTime.UtcNow,
                        PessoaId = pessoaService.Adicionar(alunoModel.Pessoa)
                    };

                    this.context.Aluno.Add(aluno);
                    this.context.SaveChanges();

                    var matricula = new MatriculaAluno()
                    {
                        SituacaoMatriculaId = alunoModel.SituacaoMatriculaId,
                      //Ativo = alunoModel.Ativo,
                        CancelamentoDate = DateTime.UtcNow,
                        CriacaoDate = DateTime.UtcNow,
                        AlunoId = aluno.AlunoId,
                    };

                    this.context.MatriculaAluno.Add(matricula);

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

        public void Alterar(Guid alunoId, AlunoBindingModel alunoModel)
        {
            var aluno = this.context.Aluno
                .Where(x => x.AlunoId.Equals(alunoId))
                .FirstOrDefault();

            aluno.NumeroCartao = alunoModel.NumeroCartao;

            aluno.AlteracaoData = DateTime.UtcNow;

            this.pessoaService.Alterar(aluno.PessoaId, alunoModel.Pessoa);

            this.context.SaveChanges();

        }

        public void Delete(Guid alunoId)
        {
            var aluno = this.context.Aluno
                .Where(x => x.AlunoId.Equals(alunoId))
                .FirstOrDefault();

            var matricula = this.context.MatriculaAluno
                .Where(x => x.AlunoId.Equals(aluno.AlunoId))
                .FirstOrDefault();

            if(matricula != null)
            {
                this.context.MatriculaAluno.Remove(matricula);
            }

            this.context.Aluno.Remove(aluno);

            this.pessoaService.Delete(aluno.PessoaId);

            this.context.SaveChanges();
        }

        public void Pagamento(PagamentoBindingModel model)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var pagamento = new Pagamento()
                    {
                        AlunoId = model.AlunoId,
                        ColaboradorId = model.ColaboradorId,
                        PagamentoDate = DateTime.UtcNow,
                        ValorMensalidade = model.ValorMensalidade,
                        MesId = model.MesId,
                        AnoId = model.AnoId,
                        FormaPagamentoId = model.FormaPagamentoId,
                        CriacaoDate = DateTime.UtcNow,
                        
                    };

                    this.context.Pagamento.Add(pagamento);
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
    }
}
