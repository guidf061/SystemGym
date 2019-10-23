using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Visitante
    {
        public Visitante()
        {
            GerenciarVisitante = new HashSet<GerenciarVisitante>();
        }

        public Guid VisitanteId { get; set; }
        public Guid PessoaId { get; set; }
        public string DocIdentidade { get; set; }
        public DateTime? VisitaData { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual ICollection<GerenciarVisitante> GerenciarVisitante { get; set; }
    }
}
