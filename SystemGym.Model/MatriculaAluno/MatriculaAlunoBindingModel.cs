using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Aluno
{
    public class MatriculaAlunoBindingModel
    {
        public Guid? AlunoId { get; set; }
        public int SituacaoMatriculaId { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriacaoDate { get; set; }
        public DateTime? CancelamentoDate { get; set; }
        public DateTime? AlteracaoDate { get; set; }
        public AlunoBindingModel Aluno { get; set; }
    }
}
