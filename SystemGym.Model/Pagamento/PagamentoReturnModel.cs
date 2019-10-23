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
        public string Nome { get; set; }
        public DateTime DataPagamento { get; set; }
        public string ValorMensalidade { get; set; }
        public int FormaPagamentoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public AlunoReturnModel Aluno { get; set; }
        public FormaPagamentoReturnModel FormaPagamento { get; set; }
    }
}
