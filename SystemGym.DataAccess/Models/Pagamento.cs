using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Pagamento
    {
        public Guid PagamentoId { get; set; }
        public Guid AlunoId { get; set; }
        public Guid ColaboradorId { get; set; }
        public DateTime DataPagamento { get; set; }
        public string ValorMensalidade { get; set; }
        public int MesId { get; set; }
        public int AnoId { get; set; }
        public int FormaPagamentoId { get; set; }
        public DateTime DataCriacao { get; set; }

        public virtual Aluno Aluno { get; set; }
        public virtual Ano Ano { get; set; }
        public virtual Colaborador Colaborador { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public virtual Mes Mes { get; set; }
    }
}
