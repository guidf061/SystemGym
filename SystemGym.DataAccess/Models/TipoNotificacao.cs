using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class TipoNotificacao
    {
        public TipoNotificacao()
        {
            GerenciarAluno = new HashSet<GerenciarAluno>();
        }

        public int TipoNotificacaoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<GerenciarAluno> GerenciarAluno { get; set; }
    }
}
