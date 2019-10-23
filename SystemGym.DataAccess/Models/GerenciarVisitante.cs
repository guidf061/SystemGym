using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class GerenciarVisitante
    {
        public Guid GerenciarVisitante1 { get; set; }
        public Guid VisitanteId { get; set; }
        public DateTime DataVisita { get; set; }
        public DateTime HoraVisita { get; set; }
        public int? QuantidadeVisita { get; set; }
        public string NomeVisitante { get; set; }

        public virtual Visitante Visitante { get; set; }
    }
}
