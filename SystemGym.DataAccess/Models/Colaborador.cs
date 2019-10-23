using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Colaborador
    {
        public Guid ColaboradorId { get; set; }
        public Guid PessoaId { get; set; }
        public int? FuncaoId { get; set; }
        public int? SituacaoColaboradorId { get; set; }
        public DateTime? CriacaoData { get; set; }
        public DateTime? AlteracaoData { get; set; }

        public virtual Funcao Funcao { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual SituacaoColaborador SituacaoColaborador { get; set; }
    }
}
