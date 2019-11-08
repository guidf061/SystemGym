using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montreal.Process.Sistel.Models;
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

        /// <summary>
        /// Pesquisa Usuário.
        /// </summary>
        /// <param name="model"></param>
        [HttpGet("Search")]
        public async Task<ActionResult<PagingModel<UsuarioReturnModel>>> GetAsync([FromQuery] UsuarioSearchModel model)
        {
            return this.Ok(await this.usuarioService.SearchAsync(model));
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

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(400)]
        public ActionResult Login(LoginModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var user = this.usuarioService.Login(model.UserName, model.Password);

            if (user == null)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

    }
}
