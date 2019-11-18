
using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Aluno
{
    public class AlunoSearchModel : PaginatorModel
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

    }
}
