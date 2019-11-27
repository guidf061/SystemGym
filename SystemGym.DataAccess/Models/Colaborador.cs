using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Colaborador
    {
        public Colaborador()
        {
            MatriculaColaborador = new HashSet<MatriculaColaborador>();
        }

        public Guid ColaboradorId { get; set; }
        public Guid PessoaId { get; set; }
        public int? FuncaoId { get; set; }
        public int? SituacaoColaboradorId { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime AlteracaoData { get; set; }
        public string NumeroSerieCtps { get; set; }
        public string NumeroCtps { get; set; }
        public string NumeroPisPasep { get; set; }
        public string DocIdentidade { get; set; }

        public virtual Funcao Funcao { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual SituacaoColaborador SituacaoColaborador { get; set; }
        public virtual ICollection<MatriculaColaborador> MatriculaColaborador { get; set; }
    }
}
