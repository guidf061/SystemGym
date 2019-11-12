using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class MatriculaColaborador
    {
        public Guid MatriculaColaboradorId { get; set; }
        public Guid ColaboradorId { get; set; }
        public int? SituacaoMatriculaId { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? CriacaoDate { get; set; }
        public DateTime CancelamentoDate { get; set; }

        public virtual Colaborador Colaborador { get; set; }
        public virtual SituacaoMatricula SituacaoMatricula { get; set; }
    }
}
