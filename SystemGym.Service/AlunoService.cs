using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGym.DataAccess.Models;
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
                    SituacaoAlunoId = x.SituacaoAlunoId,
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
                    SituacaoAlunoId = x.SituacaoAlunoId,
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
        public void Adicionar(AlunoBindingModel alunoModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {

                try
                {
                    var aluno = new Aluno()
                    {
                        PessoaId = pessoaService.Adicionar(alunoModel.Pessoa),
                        NumeroCartao = alunoModel.NumeroCartao,
                        CriacaoData = DateTime.UtcNow,
                        AlteracaoData = DateTime.UtcNow,
                        SituacaoAlunoId = alunoModel.SituacaoAlunoId,
                        Ativo = alunoModel.Ativo,  
                    };

                    this.context.Aluno.Add(aluno);
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

        public void Alterar(Guid alunoId, AlunoBindingModel alunoModel)
        {
            var aluno = this.context.Aluno
                .Where(x => x.AlunoId.Equals(alunoId))
                .FirstOrDefault();

            aluno.NumeroCartao = alunoModel.NumeroCartao;
            aluno.SituacaoAlunoId = alunoModel.SituacaoAlunoId;
            aluno.AlteracaoData = DateTime.UtcNow;

            this.pessoaService.Alterar(aluno.PessoaId, alunoModel.Pessoa);

            this.context.SaveChanges();


        }

        public void Delete(Guid alunoId)
        {
            var aluno = this.context.Aluno
                .Where(x => x.AlunoId.Equals(alunoId))
                .FirstOrDefault();

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
                        DataPagamento = DateTime.UtcNow,
                        ValorMensalidade = model.ValorMensalidade,
                        MesId = model.MesId,
                        AnoId = model.AnoId,
                        FormaPagamentoId = model.FormaPagamentoId,
                        DataCriacao = DateTime.UtcNow,
                        
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
