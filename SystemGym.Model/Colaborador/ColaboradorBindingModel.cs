using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Funcao;
using SystemGym.Model.Pessoa;
using SystemGym.Model.SituacaoColaborador;

namespace SystemGym.Model.Colaborador
{
    public class ColaboradorBindingModel
    {
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }
        public int FuncaoId { get; set; }
        public int SituacaoColaboradorId { get; set; }
        public  PessoaBindingModel Pessoa { get; set; }

    }   
}
