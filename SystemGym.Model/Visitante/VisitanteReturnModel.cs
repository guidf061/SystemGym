using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.GerenciarVisitante;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Visitante
{
   public class VisitanteReturnModel
    {
        public Guid VisitanteId { get; set; }
        public string DocIdentidade { get; set; }
        public DateTime? VisitaData { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }

        public PessoaReturnModel Pessoa { get; set; }
        public List<GerenciarVisitanteReturnModel> GerenciarVisitante { get; set; }
    }
}
