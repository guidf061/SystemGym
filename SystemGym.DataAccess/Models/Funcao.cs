using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Funcao
    {
        public Funcao()
        {
            Colaborador = new HashSet<Colaborador>();
        }

        public int FuncaoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Colaborador> Colaborador { get; set; }
    }
}
