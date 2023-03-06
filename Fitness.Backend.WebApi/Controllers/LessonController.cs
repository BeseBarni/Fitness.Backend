using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Backend.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly IClientBusinessLogic clientBl;

        public LessonsController(IClientBusinessLogic clientBl)
        {
            this.clientBl = clientBl;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<Lesson>>> Get(string? instructorId,string? clientId, int? cityId, int? sportId, Day? day)
        {

            var result = await clientBl.GetLessonsAsync(instructorId,clientId, cityId, sportId, day);

            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post([FromBody] Lesson lesson)
        {
            var result = await clientBl.CreateLessonAsync(lesson);

            if (result == DbResult.CREATED) return NoContent();

            if (result == DbResult.NOT_FOUND) return NotFound();

            return StatusCode(500);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] Lesson lesson)
        {
            var result = await clientBl.UpdateLessonAsync(lesson);

            if (result == DbResult.UPDATED) return NoContent();

            if(result == DbResult.NOT_FOUND) return NotFound();

            return StatusCode(500);
        }
        [HttpDelete("{lessonId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int lessonId)
        {
            var result = await clientBl.DeleteLessonAsync(lessonId);

            if (result == DbResult.DELETED) return NoContent();

            if (result == DbResult.NOT_FOUND) return NotFound();

            return StatusCode(500);
        }
    }
}
