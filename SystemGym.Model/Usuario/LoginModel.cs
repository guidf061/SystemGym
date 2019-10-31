using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Usuario
{
   public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
