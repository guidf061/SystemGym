using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class SituacaoColaborador
    {
        public SituacaoColaborador()
        {
            Colaborador = new HashSet<Colaborador>();
        }

        public int SituacaoColaboradorId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Colaborador> Colaborador { get; set; }
    }
}
