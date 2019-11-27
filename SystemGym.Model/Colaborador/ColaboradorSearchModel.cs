
using System;
using System.Collections.Generic;
using System.Text;
using SystemGym.Model.Funcao;
using SystemGym.Model.Pessoa;
using SystemGym.Model.SituacaoColaborador;

namespace SystemGym.Model.Colaborador
{
    public class ColaboradorSearchModel : PaginatorModel
    {
        public string Nome { get; set; }

        public string NumeroCtps { get; set; }


    }   
}
