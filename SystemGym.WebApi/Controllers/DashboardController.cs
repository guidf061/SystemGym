using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemGym.Model.Aluno;
using SystemGym.Model.Pagamento;
using SystemGym.Model.Pessoa;
using SystemGym.Service;

namespace SystemGym.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        /// <summary>
        /// Lista um quantitativo de alunos de casa mes .
        /// </summary>
        /// <returns>Quantidade de alunos</returns>
        [HttpGet("Quantity")]
        [ProducesResponseType(typeof(DashboardQuantityReturnModel), 200)]
        public ActionResult<DashboardQuantityReturnModel> GetQuantity()
        {
            return this.Ok(this.dashboardService.GetQuantity());
        }

    }
}
