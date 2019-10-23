using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Tipo
    {
        public Tipo()
        {
            Pessoa = new HashSet<Pessoa>();
        }

        public int TipoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}
