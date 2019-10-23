using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class FormaPagamento
    {
        public FormaPagamento()
        {
            Pagamento = new HashSet<Pagamento>();
        }

        public int FormaPagamentoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Pagamento> Pagamento { get; set; }
    }
}
