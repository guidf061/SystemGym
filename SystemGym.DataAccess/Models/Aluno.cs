using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Aluno
    {
        public Aluno()
        {
            GerenciarAluno = new HashSet<GerenciarAluno>();
            Pagamento = new HashSet<Pagamento>();
        }

        public Guid AlunoId { get; set; }
        public Guid PessoaId { get; set; }
        public string NumeroCartao { get; set; }
        public int? SituacaoAlunoId { get; set; }
        public DateTime CriacaoData { get; set; }
        public DateTime AlteracaoData { get; set; }
        public bool Ativo { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual SituacaoAluno SituacaoAluno { get; set; }
        public virtual ICollection<GerenciarAluno> GerenciarAluno { get; set; }
        public virtual ICollection<Pagamento> Pagamento { get; set; }
    }
}
