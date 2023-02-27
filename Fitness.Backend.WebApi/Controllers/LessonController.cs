using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<Lesson>>> Get(string? instructorId, int? cityId, int? sportId)
        {
            var instructor = instructorId != null;
            var city = cityId != null;
            var sport = sportId != null;
            IEnumerable<Lesson> result = new List<Lesson>();
            if (!instructor && !city && !sport)
                return BadRequest("One query parameter should be set");
            if (instructor && city && sport || instructor && !city && sport || instructor && city && !sport)
                return BadRequest("Instructor parameter should be used by itself");

            if (instructor && !city && !sport)
                result = await clientBl.GetLessonsAsync(instructorId);

            if (!instructor && city && !sport)
                result = await clientBl.GetLessonsAsync((int)cityId);

            if (!instructor && !city && sport)
                result = await clientBl.GetLessonsAsync(new Sport { Id = (int)sportId});

            if (!instructor && city && sport)
                result = await clientBl.GetLessonsAsync((int)cityId, (int)sportId);


            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<IEnumerable<Lesson>>> Get(int cityId)
        //{
        //    var result = await clientBl.GetLessonsAsync(cityId);

        //    if (result.Count() == 0)
        //        return NotFound();

        //    return Ok(result);
        //}
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<IEnumerable<Lesson>>> Get(Sport sport)
        //{
        //    var result = await clientBl.GetLessonsAsync(sport.Id);

        //    if (result.Count() == 0)
        //        return NotFound();

        ////    return Ok(result);
        ////}
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<IEnumerable<Lesson>>> Get(int cityId, int sportId)
        //{
        //    var result = await clientBl.GetLessonsAsync(cityId, sportId);

        //    if (result.Count() == 0)
        //        return NotFound();

        //    return Ok(result);
        //}
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<IEnumerable<Lesson>>> Get(string instructorId, int sportId)
        //{
        //    var result = await clientBl.GetLessonsAsync(instructorId, sportId);

        //    if (result.Count() == 0)
        //        return NotFound();

        //    return Ok(result);
        //}
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
