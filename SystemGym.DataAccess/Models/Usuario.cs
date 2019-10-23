using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Usuario
    {
        public Guid UsuarioId { get; set; }
        public Guid PessoaId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? DataAcesso { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
