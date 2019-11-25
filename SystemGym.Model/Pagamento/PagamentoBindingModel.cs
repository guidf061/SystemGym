using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Aluno;

namespace SystemGym.Model.Pagamento
{
   public class PagamentoBindingModel
    {
        public Guid AlunoId { get; set; }
        public Guid? UsuarioId { get; set; }
        public int PlanoId { get; set; }
        public string ValorMensalidade { get; set; }
        public int MesId { get; set; }
        public int AnoId { get; set; }
        public int FormaPagamentoId { get; set; }
    }
}
