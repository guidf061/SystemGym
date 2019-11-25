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
using SystemGym.Model.Mes;
using SystemGym.Model.Ano;

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
                .Include(x => x.Mes)
                .Include(x => x.Ano)
                .Include(x => x.Aluno)
                    .ThenInclude(x => x.Pessoa)
                .AsQueryable();

            if (model.AlunoId.HasValue)
            {
                query = query.Where(x => x.AlunoId.Equals(model.AlunoId.Value));
            }

            var pagingModel = new PagingModel<PagamentoReturnModel>();

            pagingModel.TotalItems = query.Count();

            if (!string.IsNullOrEmpty(model.Sort))
            {
                query = query.OrderBy(x => x.PagamentoDate);
            }

            pagingModel.Items = (await query
                .Skip(model.PageSize * (model.Page - 1))
                .Take(model.PageSize)
                .ToListAsync())
                .Select(x => new PagamentoReturnModel()
                {
                    PagamentoId = x.PagamentoId,
                    UsuarioId = x.UsuarioId,
                    ValorMensalidade = x.ValorMensalidade,
                    PagamentoDate = x.PagamentoDate,
                    FormaPagamentoId = x.FormaPagamentoId,
                    MesId = x.MesId,
                    Mes = new MesReturnModel()
                    {
                        MesId = x.Mes.MesId,
                        Descricao = x.Mes.Descricao,
                    },
                    Ano = new AnoReturnModel()
                    {
                        AnoId = x.Ano.AnoId,
                        Descricao = x.Ano.Descricao,
                    },

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
                            PermissaoId = x.Aluno.Pessoa.PermissaoId,
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
                    var dateNow = DateTime.UtcNow;
                    var pagamento = new Pagamento()
                    {
                        AlunoId = pagamentoModel.AlunoId,
                        UsuarioId = pagamentoModel.UsuarioId,
                        PlanoId = pagamentoModel.PlanoId,
                        ValorMensalidade = pagamentoModel.ValorMensalidade,
                        MesId = pagamentoModel.MesId,
                        AnoId = pagamentoModel.AnoId,
                        FormaPagamentoId = pagamentoModel.FormaPagamentoId,
                        CriacaoDate = DateTime.UtcNow,
                        PagamentoDate = new DateTime(pagamentoModel.AnoId, pagamentoModel.MesId, dateNow.Day ),
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
            pagamento.UsuarioId = pagamentoModel.UsuarioId;
            pagamento.PlanoId = pagamentoModel.PlanoId;
            pagamento.ValorMensalidade = pagamentoModel.ValorMensalidade;
            pagamento.MesId = pagamentoModel.MesId;
            pagamento.AnoId = pagamentoModel.AnoId;
            pagamento.FormaPagamentoId = pagamentoModel.FormaPagamentoId;
            pagamento.PagamentoDate = new DateTime(pagamentoModel.MesId, 01, pagamentoModel.AnoId);
            pagamento.CriacaoDate = DateTime.UtcNow;
            pagamento.AlteracaoDate = DateTime.UtcNow;

            this.context.SaveChanges();
        }

        public void Delete(Guid alunoId, Guid pagamentoId)
        {
            var pagamento = this.context.Pagamento
                .Where(x => x.AlunoId.Equals(alunoId) && x.PagamentoId.Equals(pagamentoId))
                .FirstOrDefault();

            this.context.Pagamento.Remove(pagamento);

            this.context.SaveChanges();
        }
    }
}
