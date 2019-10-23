using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemGym.Model.Colaborador;
using SystemGym.Model.Pessoa;
using SystemGym.Model.Visitante;
using SystemGym.Service;

namespace SystemGym.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly ColaboradorService colaboradorService;

        public ColaboradorController(ColaboradorService colaboradorService)
        {
            this.colaboradorService = colaboradorService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<ColaboradorReturnModel>> GetAll()
        {
            return this.Ok(this.colaboradorService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{colaboradorId}")]
        public ActionResult<ColaboradorReturnModel> Get(Guid colaboradorId)
        {
            return this.Ok(this.colaboradorService.Get(colaboradorId));
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] ColaboradorBindingModel colaboradorModel)
        {
            this.colaboradorService.Adicionar(colaboradorModel);
            return this.Ok();
        }

        // PUT api/values/5
        [HttpPut("{colaboradorId}")]
        public ActionResult Put(Guid colaboradorId, [FromBody] ColaboradorBindingModel colaboradorModel)
        {
            this.colaboradorService.Alterar(colaboradorId, colaboradorModel);
            return this.Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{colaboradorId}")]
        public ActionResult Delete(Guid colaboradorId)
        {
            this.colaboradorService.Delete(colaboradorId);
            return this.Ok();

        }
    }
}
