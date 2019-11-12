using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Matricula
    {
        public Guid MatriculaId { get; set; }
        public Guid AlunoId { get; set; }
        public DateTime CriacaoDate { get; set; }
        public DateTime? CancelamentoDate { get; set; }
        public bool Ativo { get; set; }
        public int SituacaoMatriculaId { get; set; }

        public virtual Aluno Aluno { get; set; }
        public virtual SituacaoMatricula SituacaoMatricula { get; set; }
    }
}
