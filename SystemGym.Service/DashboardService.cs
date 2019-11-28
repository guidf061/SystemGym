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

        public DashboardQuantityReturnModel GetMatriculasMes()
        {
            DateTime date = new DateTime(2019, 01, 01);
            DateTime nowDate = DateTime.Now;

            var query = this.context.Aluno
            .Include(x => x.Pessoa)
            .ToList().Select(x => new AlunoReturnModel()
            {
                AlunoId = x.AlunoId,
                NumeroCartao = x.NumeroCartao,
                CriacaoData = x.CriacaoData,
                AlteracaoData = x.AlteracaoData,
            })
            .ToList();

            int janeiro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(1))
             .Count();

            int fevereiro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(2))
             .Count();

            int marco = query
             .Where(x => x.CriacaoData.Value.Month.Equals(3))
             .Count();

            int abril = query
             .Where(x => x.CriacaoData.Value.Month.Equals(4))
             .Count();

            int maio = query
             .Where(x => x.CriacaoData.Value.Month.Equals(5))
             .Count();

            int junho = query
             .Where(x => x.CriacaoData.Value.Month.Equals(6))
             .Count();

            int julho = query
             .Where(x => x.CriacaoData.Value.Month.Equals(7))
             .Count();

            int agosto = query
             .Where(x => x.CriacaoData.Value.Month.Equals(8))
             .Count();

            int setembro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(9))
             .Count();

            int outubro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(10))
             .Count();

            int novembro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(11))
             .Count();

            int dezembro = query
             .Where(x => x.CriacaoData.Value.Month.Equals(12))
             .Count();

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



        public DashboardQuantityReturnModel GetQuantity()
        {
            int janeiro, fevereiro, marco, abril, maio, junho, julho, agosto, setembro, outubro, novembro, dezembro;
            DateTime date = new DateTime(2019, 01, 01);
            DateTime nowDate = DateTime.Now;

            var alunoAtivo = this.context.Aluno
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
                janeiro = alunoAtivo
                .Where(x => x.CriacaoData.Value.Month.Equals(1))
                .Count();
            }
            else
            {
                janeiro = 0;
            };

            if (new DateTime(2019, 2, 01).Month <= nowDate.Month)
            {
                fevereiro = alunoAtivo
                .Where(x => x.CriacaoData.Value.Month.Equals(2))
                .Count();
            }
            else
            {
                fevereiro = 0;
            };

            if (new DateTime(2019, 3, 01).Month <= nowDate.Month)
            {
                marco = alunoAtivo
               .Where(x => x.CriacaoData.Value.Month.Equals(3))
               .Count() + fevereiro;
            }
            else
            {
                marco = 0;
            }

            if (new DateTime(2019, 4, 01).Month <= nowDate.Month)
            {
                abril = alunoAtivo
               .Where(x => x.CriacaoData.Value.Month.Equals(4))
               .Count() + marco;
            }
            else
            {
                abril = 0;
            }

            if (new DateTime(2019, 5, 01).Month <= nowDate.Month)
            {
                maio = alunoAtivo
                .Where(x => x.CriacaoData.Value.Month.Equals(5))
                .Count() + abril;

            }
            else
            {
                maio = 0;
            }


            if (new DateTime(2019, 6, 01).Month <= nowDate.Month)
            {
                junho = alunoAtivo
               .Where(x => x.CriacaoData.Value.Month.Equals(6))
               .Count() + maio;
            }
            else
            {
                junho = 0;
            }

            if (new DateTime(2019, 7, 01).Month <= nowDate.Month)
            {
                julho = alunoAtivo
               .Where(x => x.CriacaoData.Value.Month.Equals(7))
               .Count() + junho;
            }
            else
            {
                julho = 0;
            }

            if (new DateTime(2019, 8, 01).Month <= nowDate.Month)
            {
                agosto = alunoAtivo
            .Where(x => x.CriacaoData.Value.Month.Equals(8))
            .Count() + julho;
            }
            else
            {
                agosto = 0;
            }

            if (new DateTime(2019, 9, 01).Month <= nowDate.Month)
            {
                setembro = alunoAtivo
              .Where(x => x.CriacaoData.Value.Month.Equals(9))
              .Count() + agosto;
            }
            else
            {
                setembro = 0;
            }

            if (new DateTime(2019, 10, 01).Month <= nowDate.Month)
            {
                outubro = alunoAtivo
              .Where(x => x.CriacaoData.Value.Month.Equals(10))
              .Count() + setembro;
            }
            else
            {
                outubro = 0;
            }

            if (new DateTime(2019, 11, 01).Month <= nowDate.Month)
            {
                novembro = alunoAtivo
            .Where(x => x.CriacaoData.Value.Month.Equals(11))
            .Count() + outubro;

            }
            else
            {
                novembro = 0;
            }


            if (new DateTime(2019, 12, 01).Month <= nowDate.Month)
            {

                dezembro = alunoAtivo
                .Where(x => x.CriacaoData.Value.Month.Equals(12))
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

        public DashboardQuantityReturnModel GetCanceladas()
        {
            DateTime date = new DateTime(2019, 01, 01);
            DateTime nowDate = DateTime.Now;

            var query = this.context.MatriculaAluno
            .Include(x => x.Aluno)
            .Where(x => x.CancelamentoDate.HasValue)
            .ToList();

            int janeiro = query
              .Where(x => x.CancelamentoDate.Value.Month.Equals(1))
             .Count();

            int fevereiro = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(2))
             .Count();

            int marco = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(3))
             .Count();

            int abril = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(4))
             .Count();

            int maio = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(5))
             .Count();

            int junho = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(6))
             .Count();

            int julho = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(7))
             .Count();

            int agosto = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(8))
             .Count();

            int setembro = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(9))
             .Count();

            int outubro = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(10))
             .Count();

            int novembro = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(11))
             .Count();

            int dezembro = query
             .Where(x => x.CancelamentoDate.Value.Month.Equals(12))
             .Count();

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

            int plano = 100;

            int janeiro, fevereiro, marco, abril, maio, junho, julho, agosto, setembro, outubro, novembro, dezembro;

            var alunoAtivo = this.context.Aluno
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
                janeiro = alunoAtivo
                .Where(x => x.CriacaoData.Value.Month.Equals(1))
                .Count();
            }
            else
            {
                janeiro = 0;
            };

            if (new DateTime(2019, 2, 01).Month <= nowDate.Month)
            {
                fevereiro = alunoAtivo
                .Where(x => x.CriacaoData.Value.Month.Equals(2))
                .Count();
            }
            else
            {
                fevereiro = 0;
            };

            if (new DateTime(2019, 3, 01).Month <= nowDate.Month)
            {
                marco = alunoAtivo
               .Where(x => x.CriacaoData.Value.Month.Equals(3))
               .Count() + fevereiro;
            }
            else
            {
                marco = 0;
            }

            if (new DateTime(2019, 4, 01).Month <= nowDate.Month)
            {
                abril = alunoAtivo
               .Where(x => x.CriacaoData.Value.Month.Equals(4))
               .Count() + marco;
            }
            else
            {
                abril = 0;
            }

            if (new DateTime(2019, 5, 01).Month <= nowDate.Month)
            {
                maio = alunoAtivo
                .Where(x => x.CriacaoData.Value.Month.Equals(5))
                .Count() + abril;

            }
            else
            {
                maio = 0;
            }


            if (new DateTime(2019, 6, 01).Month <= nowDate.Month)
            {
                junho = alunoAtivo
               .Where(x => x.CriacaoData.Value.Month.Equals(6))
               .Count() + maio;
            }
            else
            {
                junho = 0;
            }

            if (new DateTime(2019, 7, 01).Month <= nowDate.Month)
            {
                julho = alunoAtivo
               .Where(x => x.CriacaoData.Value.Month.Equals(7))
               .Count() + junho;
            }
            else
            {
                julho = 0;
            }

            if (new DateTime(2019, 8, 01).Month <= nowDate.Month)
            {
                agosto = alunoAtivo
            .Where(x => x.CriacaoData.Value.Month.Equals(8))
            .Count() + julho;
            }
            else
            {
                agosto = 0;
            }

            if (new DateTime(2019, 9, 01).Month <= nowDate.Month)
            {
                setembro = alunoAtivo
              .Where(x => x.CriacaoData.Value.Month.Equals(9))
              .Count() + agosto;
            }
            else
            {
                setembro = 0;
            }

            if (new DateTime(2019, 10, 01).Month <= nowDate.Month)
            {
                outubro = alunoAtivo
              .Where(x => x.CriacaoData.Value.Month.Equals(10))
              .Count() + setembro;
            }
            else
            {
                outubro = 0;
            }

            if (new DateTime(2019, 11, 01).Month <= nowDate.Month)
            {
                novembro = alunoAtivo
            .Where(x => x.CriacaoData.Value.Month.Equals(11))
            .Count() + outubro;

            }
            else
            {
                novembro = 0;
            }


            if (new DateTime(2019, 12, 01).Month <= nowDate.Month)
            {

                dezembro = alunoAtivo
                .Where(x => x.CriacaoData.Value.Month.Equals(12))
                .Count() + novembro;

            }
            else
            {
                dezembro = 0;
            }

            var quantityModel = new DashboardQuantityReturnModel
            {
                Janeiro = janeiro * 100,

                Fevereiro = fevereiro * 100,

                Marco = marco * 100,

                Abril = abril * 100,

                Maio = maio * 100,

                Junho = junho * 100,

                Julho = julho * 100,

                Agosto = agosto * 100,

                Setembro = setembro * 100,

                Outubro = outubro * 100,

                Novembro = novembro * 100,

                Dezembro = dezembro * 100,
            };

            return quantityModel;
        }
    }
}
