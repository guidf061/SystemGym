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
            .Where(x => x.MatriculaAluno.Any(i => i.Ativo.Equals(true)))
            .ToList().Select(x => new AlunoReturnModel()
            {
                AlunoId = x.AlunoId,
                NumeroCartao = x.NumeroCartao,
                CriacaoData = x.CriacaoData,
                AlteracaoData = x.AlteracaoData,
            })
            .ToList();

            int janeiro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.Month))
             .Count();

            int fevereiro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(1)))
             .Count();

            int marco = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(2)))
             .Count();

            int abril = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(3)))
             .Count();

            int maio = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(4)))
             .Count();

            int junho = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(5)))
             .Count();

            int julho = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(6)))
             .Count();

            int agosto = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(7)))
             .Count();

            int setembro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(8)))
             .Count();

            int outubro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(9)))
             .Count();

            int novembro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(10)))
             .Count();

            int dezembro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(11)))
             .Count();

            var quantityModel = new DashboardQuantityReturnModel
            {
                Janeiro = janeiro,

                Fevereiro = fevereiro + janeiro,

                Marco = marco + fevereiro,

                Abril = abril + marco,

                Maio = maio + abril,

                Junho = junho + maio,

                Julho = julho + junho,

                Agosto = agosto + julho,

                Setembro = setembro + agosto,

                Outubro = outubro + setembro,

                Novembro = novembro + outubro,

                Dezembro = dezembro + novembro,
            };

            return quantityModel;
        }

        public DashboardQuantityReturnModel GetRendimento()
        {
            DateTime date = new DateTime(2019, 01, 01);
            DateTime nowDate = DateTime.Now;

            var query = this.context.Aluno
            .Include(x => x.Pessoa)
            .Where(x => x.MatriculaAluno.Any(i => i.Ativo.Equals(true)))
            .ToList().Select(x => new AlunoReturnModel()
            {
                AlunoId = x.AlunoId,
                NumeroCartao = x.NumeroCartao,
                CriacaoData = x.CriacaoData,
                AlteracaoData = x.AlteracaoData,
            })
            .ToList();

            int janeiro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.Month))
             .Count();

            int fevereiro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(1)))
             .Count();

            int marco = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(2)))
             .Count();

            int abril = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(3)))
             .Count();

            int maio = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(4)))
             .Count();

            int junho = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(5)))
             .Count();

            int julho = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(6)))
             .Count();

            int agosto = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(7)))
             .Count();

            int setembro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(8)))
             .Count();

            int outubro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(9)))
             .Count();

            int novembro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(10)))
             .Count();

            int dezembro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(11)))
             .Count();

            var quantityModel = new DashboardQuantityReturnModel
            {
                Janeiro = janeiro,

                Fevereiro = fevereiro + janeiro,

                Marco = marco + fevereiro,

                Abril = abril + marco,

                Maio = maio + abril,

                Junho = junho + maio,

                Julho = julho + junho,

                Agosto = agosto + julho,

                Setembro = setembro + agosto,

                Outubro = outubro + setembro,

                Novembro = novembro + outubro,

                Dezembro = dezembro + novembro,
            };

            return quantityModel;
        }
    }
}
