using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class MatriculaAluno
    {
        public Guid MatriculaAlunoId { get; set; }
        public Guid AlunoId { get; set; }
        public int SituacaoMatriculaId { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriacaoDate { get; set; }
        public DateTime? CancelamentoDate { get; set; }

        public virtual Aluno Aluno { get; set; }
        public virtual SituacaoMatricula SituacaoMatricula { get; set; }
    }
}
