using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Permissao
    {
        public Permissao()
        {
            Pessoa = new HashSet<Pessoa>();
        }

        public int PermissaoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}
