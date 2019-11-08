using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Ano
    {
        public Ano()
        {
            Pagamento = new HashSet<Pagamento>();
        }

        public int AnoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Pagamento> Pagamento { get; set; }
    }
}
