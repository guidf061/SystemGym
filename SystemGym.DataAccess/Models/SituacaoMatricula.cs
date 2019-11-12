using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class SituacaoMatricula
    {
        public SituacaoMatricula()
        {
            MatriculaAluno = new HashSet<MatriculaAluno>();
            MatriculaColaborador = new HashSet<MatriculaColaborador>();
        }

        public int SituacaoMatriculaId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<MatriculaAluno> MatriculaAluno { get; set; }
        public virtual ICollection<MatriculaColaborador> MatriculaColaborador { get; set; }
    }
}
