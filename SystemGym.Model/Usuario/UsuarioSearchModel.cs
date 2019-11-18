
using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Usuario
{
   public class UsuarioSearchModel : PaginatorModel
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

    }
}
