using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class SituacaoMatricula
    {
        public SituacaoMatricula()
        {
            Matricula = new HashSet<Matricula>();
        }

        public int SituacaoMatriculaId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Matricula> Matricula { get; set; }
    }
}
