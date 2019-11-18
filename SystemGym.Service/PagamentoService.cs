using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Address;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Colaborador;
using SystemGym.Model.Pessoa;
using SystemGym.Model.Aluno;
using SystemGym.Model;

namespace SystemGym.Service
{
    public class PagamentoService
    {
        private readonly SystemGymContext context;
        private readonly PessoaService pessoaService;
        private readonly AlunoService alunoService;
        public PagamentoService(SystemGymContext context, PessoaService pessoaService, AlunoService alunoService)
        {
            this.alunoService = alunoService;
            this.context = context;
            this.pessoaService = pessoaService;
        }

        public async Task<PagingModel<PagamentoReturnModel>> SearchAsync(PagamentoSearchModel model)
        {
            var query = this.context.Pagamento
                .Include(x => x.Aluno)
                    .ThenInclude(x => x.Pessoa)
                .AsQueryable();

            if (!string.IsNullOrEmpty(model.AlunoId))
            {
                query = query.Where(x => x.AlunoId.Equals(model.AlunoId));
            }

            var pagingModel = new PagingModel<PagamentoReturnModel>();

            pagingModel.TotalItems = query.Count();

            if (!string.IsNullOrEmpty(model.Sort))
            {
                query = query.OrderBy(x => x.Aluno.Pessoa.Nome);
            }

            pagingModel.Items = (await query
                .Skip(model.PageSize * (model.Page - 1))
                .Take(model.PageSize)
                .ToListAsync())
                .Select(x => new PagamentoReturnModel()
                {
                    PagamentoId = x.PagamentoId,
                    ValorMensalidade = x.ValorMensalidade,
                    PagamentoDate = x.PagamentoDate,
                    FormaPagamentoId = x.FormaPagamentoId,
                    CriacaoDate = x.CriacaoDate,
                    Aluno = new AlunoReturnModel()
                    {
                        AlunoId = x.AlunoId,
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
                            TipoId = x.Aluno.Pessoa.TipoId,
                            AlteracaoData = x.Aluno.Pessoa.AlteracaoData,
                            CriacaoData = x.Aluno.Pessoa.CriacaoData,
                        }
                    }
                }).ToList();

            return pagingModel;
        }

        public void Adicionar(PagamentoBindingModel pagamentoModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var pagamento = new Pagamento()
                    {
                        AlunoId = pagamentoModel.AlunoId,
                        ColaboradorId = pagamentoModel.ColaboradorId,
                        PlanoId = pagamentoModel.PlanoId,
                        ValorMensalidade = pagamentoModel.ValorMensalidade,
                        MesId = pagamentoModel.MesId,
                        AnoId = pagamentoModel.AnoId,
                        FormaPagamentoId = pagamentoModel.FormaPagamentoId,
                        PagamentoDate = pagamentoModel.PagamentoDate,
                        CriacaoDate = DateTime.UtcNow
                    };

                    this.context.Pagamento.Add(pagamento);
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

        public void Alterar(Guid pagamentoId, PagamentoBindingModel pagamentoModel)
        {
            var pagamento = this.context.Pagamento
                .Where(x => x.PagamentoId.Equals(pagamentoId))
                .FirstOrDefault();

            pagamento.AlunoId = pagamentoModel.AlunoId;
            pagamento.ColaboradorId = pagamentoModel.ColaboradorId;
            pagamento.PlanoId = pagamentoModel.PlanoId;
            pagamento.ValorMensalidade = pagamentoModel.ValorMensalidade;
            pagamento.MesId = pagamentoModel.MesId;
            pagamento.AnoId = pagamentoModel.AnoId;
            pagamento.FormaPagamentoId = pagamentoModel.FormaPagamentoId;
            pagamento.PagamentoDate = DateTime.UtcNow;
            pagamento.CriacaoDate = DateTime.UtcNow;
            pagamento.AlteracaoDate = DateTime.UtcNow;

            this.context.SaveChanges();
        }

        public void Delete(Guid alunoId,Guid pagamentoId)
        {
            var pagamento = this.context.Pagamento
                .Where(x => x.AlunoId.Equals(alunoId) && x.PagamentoId.Equals(pagamentoId)) 
                .FirstOrDefault();

            this.context.Pagamento.Remove(pagamento);

            this.context.SaveChanges();
        }
    }
}
