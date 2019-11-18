using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.GerenciarVisitante;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Visitante
{
    public class VisitanteBindingModel
    {
        public string DocIdentidade { get; set; }
        public DateTime? VisitaData { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }
        public PessoaBindingModel Pessoa { get; set; }
    }
}
