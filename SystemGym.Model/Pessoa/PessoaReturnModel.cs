using System;
using System.Collections.Generic;
using System.Text;

namespace SystemGym.Model.Pessoa
{
   public class PessoaReturnModel
    {
        public Guid PessoaId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string TelefoneCelular { get; set; }
        public string TelefoneCasa { get; set; }
        public int? TipoId { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }
        public int? SexoId { get; set; }
        public string Endereco { get; set; }
    }
}
