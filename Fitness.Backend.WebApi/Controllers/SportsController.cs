using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Fitness.Backend.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly IClientBusinessLogic clientBl;

        public SportsController(IClientBusinessLogic clientBl)
        {
            this.clientBl = clientBl;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Sport>>> Get()
        {
            var result = await clientBl.GetSportsAsync();

            return Ok(result);
        }
    }
}
