
using Microsoft.AspNetCore.Mvc;
using SystemGym.Model.Address;
using SystemGym.Service;

namespace SystemGym.WebApi.Controllers
{
    /// <summary>
    /// Customer endpoint.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService addressService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressController"/> class.
        /// </summary>
        /// <param name="addressService"></param>
        public AddressController(AddressService addressService)
        {
            this.addressService = addressService;
        }

        /// <summary>
        /// Pesquisar estados.
        /// </summary>
        [HttpGet("State")]
        [ProducesResponseType(typeof(StateReturnModel), 200)]
        public ActionResult<StateReturnModel> GetState()
        {
            return this.Ok(this.addressService.GetState());
        }

        /// <summary>
        /// pegar todas as cidades.
        /// </summary>
        [HttpGet("City")]
        [ProducesResponseType(typeof(CityReturnModel), 200)]
        public ActionResult<CityReturnModel> GetCity([FromQuery] CitySearchModel searchModel)
        {
            return this.Ok(this.addressService.GetCity(searchModel));
        }
    }
}
