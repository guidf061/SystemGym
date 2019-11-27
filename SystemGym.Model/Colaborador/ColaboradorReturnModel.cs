using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Funcao;
using SystemGym.Model.Pessoa;
using SystemGym.Model.SituacaoColaborador;

namespace SystemGym.Model.Colaborador
{
   public class ColaboradorReturnModel
    {
        public Guid ColaboradorId { get; set; }
        public int? FuncaoId { get; set; }
        public int? SituacaoColaboradorId { get; set; }
        public string NumeroSerieCtps { get; set; }
        public string NumeroCtps { get; set; }
        public string NumeroPisPasep { get; set; }
        public string DocIdentidade { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }
        public PessoaReturnModel Pessoa { get; set; }
    }
}
