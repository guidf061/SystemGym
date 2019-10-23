using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class SituacaoAluno
    {
        public SituacaoAluno()
        {
            Aluno = new HashSet<Aluno>();
        }

        public int SituacaoAlunoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Aluno> Aluno { get; set; }
    }
}
