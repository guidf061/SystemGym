using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Montreal.Process.Sistel.Models;
using SystemGym.Model.Aluno;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;
using SystemGym.Service;

namespace SystemGym.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService alunoService;

        public AlunoController(AlunoService alunoService)
        {
            this.alunoService = alunoService;
        }

        /// <summary>
        /// Pesquisa Aluno.
        /// </summary>
        /// <param name="model"></param>
        [HttpGet("Search")]
        public async Task<ActionResult<PagingModel<AlunoReturnModel>>> GetAsync([FromQuery] AlunoSearchModel model)
        {
            return this.Ok(await this.alunoService.SearchAsync(model));
        }


        // GET api/values
        [HttpGet]
        public ActionResult<List<AlunoReturnModel>> GetAll()
        {
            return this.Ok(this.alunoService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{alunoId}")]
        public ActionResult<AlunoReturnModel> Get(Guid alunoId)
        {
            return this.Ok(this.alunoService.Get(alunoId));
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] AlunoBindingModel alunoModel)
        {
            this.alunoService.Adicionar(alunoModel);
            return this.Ok();
        }

        // PUT api/values/5
        [HttpPut("{alunoId}")]
        public ActionResult Put(Guid alunoId, [FromBody] AlunoBindingModel alunoModel)
        {
            this.alunoService.Alterar(alunoId, alunoModel);
            return this.Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{alunoId}")]
        public ActionResult Delete(Guid alunoId)
        {
            this.alunoService.Delete(alunoId);
            return this.Ok();

        }

        [HttpPost("Pagamento")]
        public ActionResult Post([FromBody] PagamentoBindingModel model)
        {
            this.alunoService.Pagamento(model);
            return this.Ok();
        }
    }
}
