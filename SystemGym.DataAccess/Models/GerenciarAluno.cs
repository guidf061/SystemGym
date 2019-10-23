using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class GerenciarAluno
    {
        public Guid GerenciarAlunoId { get; set; }
        public Guid AlunoId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataNotificacao { get; set; }
        public int? TipoNotificacaoId { get; set; }
        public string Email { get; set; }
        public DateTime? DataRelatorio { get; set; }

        public virtual Aluno Aluno { get; set; }
        public virtual TipoNotificacao TipoNotificacao { get; set; }
    }
}
