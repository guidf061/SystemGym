using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Aluno;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;

namespace SystemGym.Service
{
    public class DashboardService
    {
        private readonly SystemGymContext context;

        public DashboardService(SystemGymContext context, PessoaService pessoaService)
        {
            this.context = context;
        }

        public DashboardQuantityReturnModel GetQuantity()
        {
            DateTime date = new DateTime(2019, 01, 01);
            DateTime nowDate = DateTime.Now;

                var query = this.context.Aluno
                .Include(x => x.Pessoa)
                .OrderBy(x => x.CriacaoData)
                .Where(x => x.Ativo.Equals(true))
                .ToList().Select(x => new AlunoReturnModel()
                {
                    AlunoId = x.AlunoId,
                    SituacaoAlunoId = x.SituacaoAlunoId,
                    NumeroCartao = x.NumeroCartao,
                    CriacaoData = x.CriacaoData,
                    AlteracaoData = x.AlteracaoData,
                })
                .ToList();

            var quantityModel = new DashboardQuantityReturnModel
            {
                Janeiro = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.Month))
                .Count(),

                Fevereiro = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(1)))
                .Count(),

                Marco = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(2)))
                .Count(),

                Abriu = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(3)))
                .Count(),

                Maio = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(4)))
                .Count(),

                Junho = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(5)))
                .Count(),

                Julho = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(6)))
                .Count(),

                Agosto = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(7)))
                .Count(),

                Setembro = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(8)))
                .Count(),

                Outubro = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(9)))
                .Count(),

                Novembro = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(10)))
                .Count(),

                Dezembro = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(11)))
                .Count(),
            };

            return quantityModel;
        }
    }
}
