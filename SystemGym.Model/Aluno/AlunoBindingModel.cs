using System;
using System.Collections.Generic;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Aluno
{
    public class AlunoBindingModel
    {
        public Guid PessoaId { get; set; }
        public string NumeroCartao { get; set; }
        public string NumeroWhatsapp { get; set; }
        public DateTime CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }
        public PessoaBindingModel Pessoa {get; set;}
        public  List<PagamentoBindingModel> Pagamento { get; set; }
    }
}
