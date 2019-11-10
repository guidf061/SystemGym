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

                Abriu = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(3)) && x.Ativo.Equals(true))
                .Count(),

                Maio = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(4)) && x.Ativo.Equals(true))
                .Count(),

                Junho = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(5)) && x.Ativo.Equals(true))
                .Count(),

                Julho = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(6)) && x.Ativo.Equals(true))
                .Count(),

                Agosto = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(7)) && x.Ativo.Equals(true))
                .Count(),

                Setembro = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(8)) && x.Ativo.Equals(true))
                .Count(),

                Outubro = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(9)) && x.Ativo.Equals(true))
                .Count(),

                Novembro = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(10)) && x.Ativo.Equals(true))
                .Count(),

                Dezembro = this.context.Aluno
                .Where(x => x.Pessoa.CriacaoData.Value.Month.Equals(janeiro.AddMonths(11)) && x.Ativo.Equals(true))
                .Count(),
            };

            return quantityModel;
        }
    }
}
