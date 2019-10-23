using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Pagamento;

namespace SystemGym.Model.FormaPagamento
{
    public class FormaPagamentoBinding
    {
        public string Descricao { get; set; }
        public List<PagamentoBindingModel> Pagamento { get; set; }
    }
}
