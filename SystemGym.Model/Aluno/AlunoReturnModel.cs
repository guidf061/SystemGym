using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Aluno
{
    public class AlunoReturnModel
    {
        public Guid AlunoId { get; set; }
        public Guid PessoaId { get; set; }
        public string NumeroCartao { get; set; }
        public string NumeroWhatsapp { get; set; }
        public int SituacaoMatriculaId { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }
        public PessoaReturnModel Pessoa { get; set; }
        public List<PagamentoReturnModel> Pagamento { get; set; }
    }
}
