using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Aluno;
using SystemGym.Model.FormaPagamento;

namespace SystemGym.Model.Pagamento
{
    public class PagamentoReturnModel
    {
        public Guid PagamentoId { get; set; }
        public Guid AlunoId { get; set; }
        public Guid ColaboradorId { get; set; }
        public int PlanoId { get; set; }
        public string ValorMensalidade { get; set; }
        public int MesId { get; set; }
        public int AnoId { get; set; }
        public int FormaPagamentoId { get; set; }
        public DateTime PagamentoDate { get; set; }
        public DateTime CriacaoDate { get; set; }
        public AlunoReturnModel Aluno { get; set; }
        public FormaPagamentoReturnModel FormaPagamento { get; set; }
    }
}
