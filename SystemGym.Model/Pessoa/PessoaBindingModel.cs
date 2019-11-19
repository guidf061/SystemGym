using System;
using System.Collections.Generic;
using System.Text;

namespace SystemGym.Model.Pessoa
{
    public class PessoaBindingModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public int SexoId { get; set; }
        public string Endereco { get; set; }
        public int? PermissaoId { get; set; }
        public string TelefoneCelular { get; set; }
        public string TelefoneCasa { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
    }
}
