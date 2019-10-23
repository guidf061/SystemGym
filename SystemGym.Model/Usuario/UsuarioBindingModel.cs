using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Usuario
{
   public class UsuarioBindingModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? DataAcesso { get; set; }
        public DateTime? DataCadastro { get; set; }
        public PessoaBindingModel Pessoa { get; set; }
    }
}
