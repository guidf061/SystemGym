using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemGym.Model.Pessoa;
using SystemGym.Model.Visitante;
using SystemGym.Service;

namespace SystemGym.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitanteController : ControllerBase
    {
        private readonly VisitanteService visitanteService;

        public VisitanteController(VisitanteService visitanteService)
        {
            this.visitanteService = visitanteService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<VisitanteReturnModel>> GetAll()
        {
            return this.Ok(this.visitanteService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{visitanteId}")]
        public ActionResult<VisitanteReturnModel> Get(Guid visitanteId)
        {
            return this.Ok(this.visitanteService.Get(visitanteId));
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] VisitanteBindingModel visitanteModel)
        {
            this.visitanteService.Adicionar(visitanteModel);
            return this.Ok();
        }

        // PUT api/values/5
        [HttpPut("{visitanteId}")]
        public ActionResult Put(Guid visitanteId, [FromBody] VisitanteBindingModel visitanteModel)
        {
            this.visitanteService.Alterar(visitanteId, visitanteModel);
            return this.Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{visitanteId}")]
        public ActionResult Delete(Guid visitanteId)
        {
            this.visitanteService.Delete(visitanteId);
            return this.Ok();

        }
    }
}
