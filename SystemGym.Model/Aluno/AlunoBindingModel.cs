using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Aluno
{
    public class AlunoBindingModel
    {
        public string NumeroCartao { get; set; }
        public int SituacaoAlunoId { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }
        public PessoaBindingModel Pessoa {get; set;}
        public List<PagamentoBindingModel> Pagamento { get; set; }

    }
}
