using System;
using SystemGym.Model.SituacaoMatricula;

namespace SystemGym.Model.Aluno
{
    public class MatriculaAlunoReturnModel
    {
        public Guid MatriculaAlunoId { get; set; }
        public Guid AlunoId { get; set; }
        public int SituacaoMatriculaId { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriacaoDate { get; set; }
        public DateTime? CancelamentoDate { get; set; }
        public DateTime? AlteracaoDate { get; set; }
        public AlunoReturnModel Aluno { get; set; }
    }
}
