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

        /// <summary>
        /// Lista a quantidades de matriculas feita no mes .
        /// </summary>
        /// <returns>Quantidade de matriculas</returns>
        [HttpGet("QuantityMatriculasMes")]
        [ProducesResponseType(typeof(DashboardQuantityReturnModel), 200)]
        public ActionResult<DashboardQuantityReturnModel> GetMatriculasMes()
        {
            return this.Ok(this.dashboardService.GetMatriculasMes());
        }

        /// <summary>
        /// Lista a quantidades de matriculas canceladas no mes .
        /// </summary>
        /// <returns>Quantidade de matriculas</returns>
        [HttpGet("QuantityMatriculasCanceladas")]
        [ProducesResponseType(typeof(DashboardQuantityReturnModel), 200)]
        public ActionResult<DashboardQuantityReturnModel> GetMatriculasCanceladas()
        {
          return this.Ok(this.dashboardService.GetCanceladas());
        }

        /// <summary>
        /// Lista a o rendimento do mes .
        /// </summary>
        /// <returns>Quantidade de matriculas</returns>
        [HttpGet("GetRendimento")]
        [ProducesResponseType(typeof(DashboardQuantityReturnModel), 200)]
        public ActionResult<DashboardQuantityReturnModel> GetRendimento()
        {
            return this.Ok(this.dashboardService.GetRendimento());
        }

        /// <summary>
        /// Lista a o rendimento do mes .
        /// </summary>
        /// <returns>Quantidade de matriculas</returns>
        [HttpGet("GetInadiplentes")]
        [ProducesResponseType(typeof(DashboardQuantityReturnModel), 200)]
        public ActionResult<DashboardQuantityReturnModel> GetInadiplentes()
        {
            return this.Ok(this.dashboardService.GetInadiplentes());
        }
    }
}
