
using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.GerenciarVisitante;
using SystemGym.Model.Pessoa;

namespace SystemGym.Model.Visitante
{
    public class VisitanteSearchModel : PaginatorModel
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }
    }
}
