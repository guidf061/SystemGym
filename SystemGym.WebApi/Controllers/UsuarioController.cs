using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemGym.Model.Usuario;
using SystemGym.Service;

namespace SystemGym.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<List<UsuarioReturnModel>> GetAll()
        {
            return this.Ok(this.usuarioService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{usuarioId}")]
        public ActionResult<UsuarioReturnModel> Get(Guid usuarioId)
        {
            return this.Ok(this.usuarioService.Get(usuarioId));
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] UsuarioBindingModel usuarioModel)
        {
            this.usuarioService.Adicionar(usuarioModel);
            return this.Ok();
        }

        // PUT api/values/5
        [HttpPut("{usuarioId}")]
        public ActionResult Put(Guid usuarioId, [FromBody] UsuarioBindingModel usuarioModel)
        {
            this.usuarioService.Alterar(usuarioId, usuarioModel);
            return this.Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{usuarioId}")]
        public ActionResult Delete(Guid usuarioId)
        {
            this.usuarioService.Delete(usuarioId);
            return this.Ok();
        }
    }
}
