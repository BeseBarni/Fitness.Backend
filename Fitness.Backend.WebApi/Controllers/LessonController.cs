using AutoMapper;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Backend.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonBusinessLogic bl;
        private readonly IMapper mapper;

        public LessonsController(ILessonBusinessLogic bl, IMapper mapper)
        {
            this.bl = bl;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<LessonData>>> Get(string? instructorId, string? sportId, Day? day)
        {

            var result = await bl.GetAll(new LessonData { InstructorId = instructorId,SportId = sportId, Day = day});

            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{lessonId}/Clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<LessonData>>> GetUsers(string lessonId)
        {

            var result = await bl.GetLessonUsers(lessonId);

            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post([FromBody] LessonData lesson)
        {

            await bl.Add(lesson);

            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Instructor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] LessonData lesson)
        {
            await bl.Update(lesson);

            return NoContent();
        }
        [HttpDelete("{lessonId}")]
        [Authorize(Roles = "Instructor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string lessonId)
        {
            await bl.Delete(lessonId);

            return NoContent();
        }
    }
}
