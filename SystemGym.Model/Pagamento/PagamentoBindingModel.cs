using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Aluno;

namespace SystemGym.Model.Pagamento
{
   public class PagamentoBindingModel
    {
        public Guid PagamentoId { get; set; }
        public string Nome { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string ValorMensalidade { get; set; }
        public int FormaPagamentoId { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
