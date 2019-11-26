using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Mes
    {
        public Mes()
        {
            Pagamento = new HashSet<Pagamento>();
        }

        public int MesId { get; set; }
        public string Descricao { get; set; }
        public DateTime? CriacaoDate { get; set; }

        public virtual ICollection<Pagamento> Pagamento { get; set; }
    }
}
