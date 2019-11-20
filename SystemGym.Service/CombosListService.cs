using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Address;
using SystemGym.Model.Ano;
using SystemGym.Model.FormaPagamento;
using SystemGym.Model.Funcao;
using SystemGym.Model.Mes;
using SystemGym.Model.Permissao;
using SystemGym.Model.Plano;
using SystemGym.Model.Sexo;
using SystemGym.Model.SituacaoAluno;
using SystemGym.Model.SituacaoColaborador;
using SystemGym.Model.SituacaoMatricula;
using SystemGym.Model.TipoNotificacao;

namespace SystemGym.Service
{
    public class CombosListService
    {
        private readonly SystemGymContext context;

        public CombosListService(SystemGymContext context)
        {
            this.context = context;
        }

        public List<SexoReturnModel> GetSexo()
        {
            return this.context.Sexo
               .ToList()
               .Select(x => new SexoReturnModel() {
                   SexoId = x.SexoId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }
        public List<PermissaoReturnModel> GetPermissao()
        {
            return this.context.Permissao
               .ToList()
               .Select(x => new PermissaoReturnModel()
               {
                   PermissaoId = x.PermissaoId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }

        public List<SituacaoColaboradorReturnModel> GetSituacaoColaborador()
        {
            return this.context.SituacaoColaborador
               .ToList()
               .Select(x => new SituacaoColaboradorReturnModel()
               {
                   SituacaoColaboradorId = x.SituacaoColaboradorId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }

        public List<SituacaoMatriculaReturnModel> GetSituacaoMatricula()
        {
            return this.context.SituacaoMatricula
               .ToList()
               .Select(x => new SituacaoMatriculaReturnModel()
               {
                   SituacaoMatriculaId = x.SituacaoMatriculaId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }

        public List<TipoNotificacaoReturnModel> GetTipoNotificacao()
        {
            return this.context.TipoNotificacao
               .ToList()
               .Select(x => new TipoNotificacaoReturnModel()
               {
                   TipoNotificacaoId = x.TipoNotificacaoId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }

        public List<PlanoReturnModel> GetPlano()
        {
            return this.context.Plano
               .ToList()
               .Select(x => new PlanoReturnModel()
               {
                   PlanoId = x.PlanoId,
                   Descricao = x.Descricao,
                   Valor = x.Valor,
               })
               .ToList();
        }

        public List<FuncaoReturnModel> GetFuncao()
        {
            return this.context.Funcao
               .ToList()
               .Select(x => new FuncaoReturnModel()
               {
                   FuncaoId = x.FuncaoId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }

        public List<MesReturnModel> GetMes()
        {
            return this.context.Mes
               .ToList()
               .Select(x => new MesReturnModel()
               {
                   MesId = x.MesId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }

        public List<AnoReturnModel> GetAno()
        {
            return this.context.Ano
               .ToList()
               .Select(x => new AnoReturnModel()
               {
                   AnoId = x.AnoId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }

        public List<FormaPagamentoReturnModel> GetFormaPagamento()
        {
            return this.context.FormaPagamento
               .ToList()
               .Select(x => new FormaPagamentoReturnModel()
               {
                   FormaPagamentoId = x.FormaPagamentoId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }

    }
}
