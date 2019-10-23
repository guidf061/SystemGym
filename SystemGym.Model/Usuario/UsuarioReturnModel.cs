using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Usuario
{
   public class UsuarioReturnModel
    {
        public Guid UsuarioId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? DataAcesso { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public PessoaReturnModel Pessoa { get; set; }
    }
}
