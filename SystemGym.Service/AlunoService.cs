using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGym.DataAccess.Models;
using SystemGym.Model;
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
                        PermissaoId = x.Pessoa.PermissaoId,
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
                        PermissaoId = x.Pessoa.PermissaoId,
                        AlteracaoData = x.Pessoa.AlteracaoData,
                        CriacaoData = x.Pessoa.CriacaoData,
                    }
                })
                .FirstOrDefault();
        }

        public async Task<PagingModel<MatriculaAlunoReturnModel>> SearchAsync(MatriculaAlunoSearchModel model)
        {
            var query = this.context.MatriculaAluno
                .Include(x => x.Aluno)
                    .ThenInclude(x => x.Pessoa)
                       .ThenInclude(x => x.City)
                       .Where(x => !x.CancelamentoDate.HasValue)
                .AsQueryable();

            if (!string.IsNullOrEmpty(model.Nome))
            {
                query = query.Where(x => EF.Functions.Like(x.Aluno.Pessoa.Nome, "%" + model.Nome + "%"));
            }

            if (!string.IsNullOrEmpty(model.Cpf))
            {
                query = query.Where(x => EF.Functions.Like(x.Aluno.Pessoa.Cpf, "%" + model.Cpf + "%"));
            }

            var pagingModel = new PagingModel<MatriculaAlunoReturnModel>();

            pagingModel.TotalItems = query.Count();

            if (!string.IsNullOrEmpty(model.Sort))
            {
                query = query.OrderBy(x => x.Aluno.Pessoa.Nome);
            }

            pagingModel.Items = (await query
                .Skip(model.PageSize * (model.Page - 1))
                .Take(model.PageSize)
                .ToListAsync())
                .Select(x => new MatriculaAlunoReturnModel()
                {

                    MatriculaAlunoId = x.MatriculaAlunoId,
                    AlunoId = x.AlunoId,
                    Ativo = x.Ativo,
                    SituacaoMatriculaId = x.SituacaoMatriculaId,
                    CancelamentoDate = x.CancelamentoDate,
                    AlteracaoDate = x.AlteracaoDate,
                    CriacaoDate = x.CriacaoDate,
                    Aluno = new AlunoReturnModel()
                    {
                        AlunoId = x.Aluno.AlunoId,
                        PessoaId = x.Aluno.PessoaId,
                        NumeroWhatsapp = x.Aluno.NumeroWhatsapp,
                        AlteracaoData = x.Aluno.AlteracaoData,
                        CriacaoData = x.Aluno.CriacaoData,
                        NumeroCartao = x.Aluno.NumeroCartao,
                        Pessoa = new PessoaReturnModel()
                        {
                            PessoaId = x.Aluno.Pessoa.PessoaId,
                            Nome = x.Aluno.Pessoa.Nome,
                            Cpf = x.Aluno.Pessoa.Cpf,
                            Email = x.Aluno.Pessoa.Email,
                            TelefoneCasa = x.Aluno.Pessoa.TelefoneCasa,
                            TelefoneCelular = x.Aluno.Pessoa.TelefoneCelular,
                            Endereco = x.Aluno.Pessoa.Endereco,
                            SexoId = x.Aluno.Pessoa.SexoId,
                            PermissaoId = x.Aluno.Pessoa.PermissaoId,
                            DataNascimento = x.Aluno.Pessoa.DataNascimento,
                            AlteracaoData = x.Aluno.Pessoa.AlteracaoData,
                            CriacaoData = x.Aluno.Pessoa.CriacaoData,
                            City = x.Aluno.Pessoa.City == null ? null : new CityReturnModel()
                            {
                                CityId = x.Aluno.Pessoa.City.CityId,
                                Name = x.Aluno.Pessoa.City.Name,
                                StateId = x.Aluno.Pessoa.City.StateId,
                                State = x.Aluno.Pessoa.City.State == null ? null : new StateReturnModel()
                                {
                                    StateId = x.Aluno.Pessoa.City.State.StateId,
                                    Acronym = x.Aluno.Pessoa.City.State.Acronym,
                                    Name = x.Aluno.Pessoa.City.State.Name,
                                    CountryId = x.Aluno.Pessoa.City.State.CountryId,
                                }
                            }
                        }
                    }
                }).ToList();

            return pagingModel;
        }
        public void Adicionar(MatriculaAlunoBindingModel model)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {

                try
                {
                    var aluno = new Aluno()
                    {
                        NumeroCartao = model.Aluno.NumeroCartao,
                        NumeroWhatsapp = model.Aluno.NumeroWhatsapp,
                        CriacaoData = DateTime.UtcNow,
                        AlteracaoData = DateTime.UtcNow,
                        PessoaId = pessoaService.Adicionar(model.Aluno.Pessoa)
                    };

                    this.context.Aluno.Add(aluno);
                    this.context.SaveChanges();

                    var matricula = new MatriculaAluno()
                    {
                        SituacaoMatriculaId = model.SituacaoMatriculaId,
                        Ativo = model.Ativo,
                        CancelamentoDate = model.CancelamentoDate,
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

        public void Alterar(Guid matriculaAlunoId, MatriculaAlunoBindingModel model)
        {
            var matriculaAluno = this.context.MatriculaAluno
                .Include(x => x.Aluno)
                .Where(x => x.MatriculaAlunoId.Equals(matriculaAlunoId) && x.AlunoId.Equals(model.AlunoId))
                .FirstOrDefault();

            //matricula
            matriculaAluno.SituacaoMatriculaId = model.SituacaoMatriculaId;

            matriculaAluno.Ativo = model.Ativo;

            matriculaAluno.AlteracaoDate = DateTime.UtcNow;


            //aluno

            matriculaAluno.Aluno.NumeroCartao = model.Aluno.NumeroCartao;

            matriculaAluno.Aluno.NumeroWhatsapp = model.Aluno.NumeroWhatsapp;

            matriculaAluno.Aluno.AlteracaoData = model.Aluno.AlteracaoData;


            this.pessoaService.Alterar(model.Aluno.PessoaId, model.Aluno.Pessoa);

            this.context.SaveChanges();

        }

        public void Delete(Guid alunoId)
        {
            var matricula = this.context.MatriculaAluno
                .Where(x => x.AlunoId.Equals(alunoId))
                .FirstOrDefault();

            matricula.CancelamentoDate = DateTime.UtcNow;
           
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
                        UsuarioId = model.UsuarioId,
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
