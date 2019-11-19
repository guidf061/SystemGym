using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Address;
using SystemGym.Model.Permissao;
using SystemGym.Model.Sexo;

namespace SystemGym.Model.Pessoa
{
   public class PessoaReturnModel
    {
        public Guid PessoaId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public int? SexoId { get; set; }
        public string Endereco { get; set; }
        public int? PermissaoId { get; set; }
        public string TelefoneCelular { get; set; }
        public string TelefoneCasa { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public CityReturnModel City { get; set; }
        public SexoReturnModel Sexo { get; set; }
        public PermissaoReturnModel Permissao { get; set; }
    }
}
