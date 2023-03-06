using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity;
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

        [HttpDelete("{sportId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int sportId)
        {
            var result = await clientBl.DeleteSportAsync(sportId);

            if (result == DbResult.DELETED) return NoContent();

            if (result == DbResult.NOT_FOUND) return NotFound();

            return StatusCode(500);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post([FromBody] Sport sport)
        {
            var result = await clientBl.CreateSportAsync(sport);

            if (result == DbResult.CREATED) return NoContent();

            if (result == DbResult.NOT_FOUND) return NotFound();

            return StatusCode(500);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] Sport sport)
        {
            var result = await clientBl.UpdateSportAsync(sport);

            if (result == DbResult.UPDATED) return NoContent();

            if (result == DbResult.NOT_FOUND) return NotFound();

            return StatusCode(500);
        }
    }
}
