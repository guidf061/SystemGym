using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Aluno = new HashSet<Aluno>();
            Colaborador = new HashSet<Colaborador>();
            Usuario = new HashSet<Usuario>();
            Visitante = new HashSet<Visitante>();
        }

        public Guid PessoaId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public int SexoId { get; set; }
        public int? PermissaoId { get; set; }
        public string Endereco { get; set; }
        public string TelefoneCelular { get; set; }
        public string TelefoneCasa { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Permissao Permissao { get; set; }
        public virtual Sexo Sexo { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Aluno> Aluno { get; set; }
        public virtual ICollection<Colaborador> Colaborador { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Visitante> Visitante { get; set; }
    }
}
