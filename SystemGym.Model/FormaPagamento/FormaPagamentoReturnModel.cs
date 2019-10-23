using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Pagamento;

namespace SystemGym.Model.FormaPagamento
{
   public class FormaPagamentoReturnModel
   {
        public string Descricao { get; set; }

        public List<PagamentoReturnModel> Pagamento { get; set; }
    }
}
