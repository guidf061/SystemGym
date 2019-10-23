using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Sexo
    {
        public Sexo()
        {
            Pessoa = new HashSet<Pessoa>();
        }

        public int SexoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}
