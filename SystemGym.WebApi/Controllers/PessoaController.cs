using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemGym.Model.Pessoa;
using SystemGym.Service;

namespace SystemGym.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService pessoaService;

        public PessoaController(PessoaService pessoaService)
        {
            this.pessoaService = pessoaService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<PessoaReturnModel>> GetAll()
        {
            return this.Ok(this.pessoaService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{pessoaId}")]
        public ActionResult<PessoaReturnModel> Get(Guid pessoaId)
        {
            return this.Ok(this.pessoaService.Get(pessoaId));
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] PessoaBindingModel pessoaModel)
        {
            this.pessoaService.Adicionar(pessoaModel);
            return this.Ok();
        }

        // PUT api/values/5
        [HttpPut("{pessoaId}")]
        public ActionResult Put(Guid pessoaId, [FromBody] PessoaBindingModel pessoaModel)
        {
            this.pessoaService.Alterar(pessoaId, pessoaModel);
            return this.Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid pessoaId)
        {
            this.pessoaService.Delete(pessoaId);
            return this.Ok();

        }
    }
}
