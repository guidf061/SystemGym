using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Plano
    {
        public Plano()
        {
            Pagamento = new HashSet<Pagamento>();
        }

        public int PlanoId { get; set; }
        public string Descricao { get; set; }
        public int Valor { get; set; }

        public virtual ICollection<Pagamento> Pagamento { get; set; }
    }
}
