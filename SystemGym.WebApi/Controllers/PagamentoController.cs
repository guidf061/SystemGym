using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemGym.Model;
using SystemGym.Model.Aluno;
using SystemGym.Model.Pagamento;
using SystemGym.Service;

namespace SystemGym.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly PagamentoService pagamentoService;

        public PagamentoController(PagamentoService pagamentoService)
        {
            this.pagamentoService = pagamentoService;
        }

        /// <summary>
        /// Pesquisa Pagamento.
        /// </summary>
        /// <param name="model"></param>
        [HttpGet("Search")]
        public async Task<ActionResult<PagingModel<PagamentoReturnModel>>> GetAsync([FromQuery] PagamentoSearchModel model)
        {
            return this.Ok(await this.pagamentoService.SearchAsync(model));
        }


        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] PagamentoBindingModel pagamentoModel)
        {
            this.pagamentoService.Adicionar(pagamentoModel);
            return this.Ok();
        }

        // PUT api/values/5
        [HttpPut("{pagamentoId}")]
        public ActionResult Put(Guid pagamentoId, [FromBody] PagamentoBindingModel pagamentoModel)
        {
            this.pagamentoService.Alterar(pagamentoId, pagamentoModel);
            return this.Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{pagamentoId}")]
        public ActionResult Delete(Guid alunoId,Guid pagamentoId)
        {
            this.pagamentoService.Delete(alunoId, pagamentoId);
            return this.Ok();

        }
    }
}
