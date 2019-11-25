
using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Aluno
{
    public class PagamentoSearchModel : PaginatorModel
    {
        public Guid? AlunoId { get; set; }
    }
}
