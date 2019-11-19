
using Microsoft.AspNetCore.Mvc;
using SystemGym.Model.Address;
using SystemGym.Model.Permissao;
using SystemGym.Model.Sexo;
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
    }
}
