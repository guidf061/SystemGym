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
        private readonly PessoaService pessoaService;
        private readonly AlunoService  alunoService;

        public DashboardService(SystemGymContext context, PessoaService pessoaService)
        {
            this.context = context;
            this.pessoaService = pessoaService;
        }

        public DashboardQuantityReturnModel GetQuantity()
        {
            DateTime janeiro = new DateTime(2019, 01, 01);

            var quantityModel = new DashboardQuantityReturnModel
            {
                Janeiro = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.Month) && x.Ativo.Equals(true))
                .Count(),

                Fevereiro = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(1)) && x.Ativo.Equals(true))
                .Count(),

                Marco = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(2)) && x.Ativo.Equals(true))
                .Count(),
            };

            return quantityModel;
        }
    }
}
