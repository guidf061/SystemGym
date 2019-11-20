
using Microsoft.AspNetCore.Mvc;
using SystemGym.Model.Address;
using SystemGym.Model.Ano;
using SystemGym.Model.FormaPagamento;
using SystemGym.Model.Funcao;
using SystemGym.Model.Mes;
using SystemGym.Model.Permissao;
using SystemGym.Model.Plano;
using SystemGym.Model.Sexo;
using SystemGym.Model.SituacaoColaborador;
using SystemGym.Model.SituacaoMatricula;
using SystemGym.Model.TipoNotificacao;
using SystemGym.Service;

namespace SystemGym.WebApi.Controllers
{
    /// <summary>
    /// Customer endpoint.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CombosListController : ControllerBase
    {
        private readonly CombosListService combosListService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombosListController"/> class.
        /// </summary>
        /// <param name="addressService"></param>
        public CombosListController(CombosListService combosListService)
        {
            this.combosListService = combosListService;
        }

        /// <summary>
        /// Pesquisar estados.
        /// </summary>
        [HttpGet("Sexo")]
        [ProducesResponseType(typeof(SexoReturnModel), 200)]
        public ActionResult<SexoReturnModel> GetState()
        {
            return this.Ok(this.combosListService.GetSexo());
        }
        /// <summary>
        /// Pesquisar estados.
        /// </summary>
        [HttpGet("Permissao")]
        [ProducesResponseType(typeof(PermissaoReturnModel), 200)]
        public ActionResult<PermissaoReturnModel> GetPermissao()
        {
            return this.Ok(this.combosListService.GetPermissao());
        }

        /// <summary>
        /// Pesquisar Situacao do Colaborador.
        /// </summary>
        [HttpGet("SituacaoColaborador")]
        [ProducesResponseType(typeof(PermissaoReturnModel), 200)]
        public ActionResult<SituacaoColaboradorReturnModel> GetSituacaoColaborador()
        {
            return this.Ok(this.combosListService.GetSituacaoColaborador());
        }

        /// <summary>
        /// Pesquisar Situacao da matricula.
        /// </summary>
        [HttpGet("SituacaoMatricula")]
        [ProducesResponseType(typeof(SituacaoMatriculaReturnModel), 200)]
        public ActionResult<SituacaoMatriculaReturnModel> GetSituacaoMatricula()
        {
            return this.Ok(this.combosListService.GetSituacaoMatricula());
        }

        /// <summary>
        /// Pesquisar Tipo de Notificacao.
        /// </summary>
        [HttpGet("TipoNotificacao")]
        [ProducesResponseType(typeof(TipoNotificacaoReturnModel), 200)]
        public ActionResult<TipoNotificacaoReturnModel> GetTipoNotificacao()
        {
            return this.Ok(this.combosListService.GetTipoNotificacao());
        }

        /// <summary>
        /// Pesquisar Plano.
        /// </summary>
        [HttpGet("Plano")]
        [ProducesResponseType(typeof(PlanoReturnModel), 200)]
        public ActionResult<PlanoReturnModel> GetPlano()
        {
            return this.Ok(this.combosListService.GetPlano());
        }

        /// <summary>
        /// Pesquisar Funcao.
        /// </summary>
        [HttpGet("Funcao")]
        [ProducesResponseType(typeof(FuncaoReturnModel), 200)]
        public ActionResult<FuncaoReturnModel> GetFuncao()
        {
            return this.Ok(this.combosListService.GetFuncao());
        }

        /// <summary>
        /// Pesquisar Mes.
        /// </summary>
        [HttpGet("Mes")]
        [ProducesResponseType(typeof(MesReturnModel), 200)]
        public ActionResult<MesReturnModel> GetMes()
        {
            return this.Ok(this.combosListService.GetMes());
        }

        /// <summary>
        /// Pesquisar Ano.
        /// </summary>
        [HttpGet("Ano")]
        [ProducesResponseType(typeof(AnoReturnModel), 200)]
        public ActionResult<AnoReturnModel> GetAno()
        {
            return this.Ok(this.combosListService.GetAno());
        }

        /// <summary>
        /// Pesquisar Forma de Pagamento.
        /// </summary>
        [HttpGet("FormaPagamento")]
        [ProducesResponseType(typeof(FormaPagamentoReturnModel), 200)]
        public ActionResult<FormaPagamentoReturnModel> GetFormaPagamento()
        {
            return this.Ok(this.combosListService.GetFormaPagamento());
        }
    }
}
