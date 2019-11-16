﻿using Microsoft.EntityFrameworkCore;
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
            int janeiro, fevereiro, marco, abril, maio, junho, julho, agosto, setembro, outubro, novembro, dezembro;
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

            if (new DateTime(2019, 1, 01).Month <= nowDate.Month)
            {
                janeiro = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.Month))
                .Count();
            }
            else
            {
                janeiro = 0;
            };

            if (new DateTime(2019, 2, 01).Month <= nowDate.Month)
            {
                fevereiro = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.Month))
                .Count();
            }
            else
            {
                fevereiro = 0;
            };

            if (new DateTime(2019, 3, 01).Month <= nowDate.Month)
            {
                marco = query
               .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(2)))
               .Count() + fevereiro;
            }
            else
            {
                marco = 0;
            }

            if (new DateTime(2019, 4, 01).Month <= nowDate.Month)
            {
                abril = query
               .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(3)))
               .Count() + marco;
            }
            else
            {
                abril = 0;
            }

            if (new DateTime(2019, 5, 01).Month <= nowDate.Month)
            {
                maio = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(4)))
                .Count() + abril;

            }
            else
            {
                maio = 0;
            }


            if (new DateTime(2019, 6, 01).Month <= nowDate.Month)
            {
                junho = query
               .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(5)))
               .Count() + maio;
            }
            else
            {
                junho = 0;
            }

            if (new DateTime(2019, 7, 01).Month <= nowDate.Month)
            {
                julho = query
               .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(6)))
               .Count() + junho;
            }
            else
            {
                julho = 0;
            }

            if (new DateTime(2019, 8, 01).Month <= nowDate.Month)
            {
                agosto = query
            .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(7)))
            .Count() + julho;
            }
            else
            {
                agosto = 0;
            }

            if (new DateTime(2019, 9, 01).Month <= nowDate.Month)
            {
                setembro = query
              .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(8)))
              .Count() + agosto;
            }
            else
            {
                setembro = 0;
            }

            if (new DateTime(2019, 10, 01).Month <= nowDate.Month)
            {
                outubro = query
              .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(9)))
              .Count() + setembro;
            }
            else
            {
                outubro = 0;
            }

            if (new DateTime(2019, 11, 01).Month <= nowDate.Month)
            {
                novembro = query
            .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(10)))
            .Count() + outubro;

            }
            else
            {
                novembro = 0;
            }


            if (new DateTime(2019, 12, 01).Month <= nowDate.Month)
            {

                dezembro = query
                .Where(x => x.CriacaoData.Value.Month.Equals(date.AddMonths(11)))
                .Count() + novembro;

            }
            else
            {
                dezembro = 0;
            }

            var quantityModel = new DashboardQuantityReturnModel
            {
                Janeiro = janeiro,

                Fevereiro = fevereiro,

                Marco = marco,

                Abril = abril,

                Maio = maio,

                Junho = junho,

                Julho = julho,

                Agosto = agosto,

                Setembro = setembro,

                Outubro = outubro,

                Novembro = novembro,

                Dezembro = dezembro,
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
