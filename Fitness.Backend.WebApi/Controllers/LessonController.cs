using AutoMapper;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity.DatabaseEntities;
using Fitness.Backend.Application.DataContracts.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Backend.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonRepository repo;
        private readonly IMapper mapper;

        public LessonsController(ILessonRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<LessonData>>> Get(string? instructorId, string? sportId, Day? day)
        {

            var result = await repo.GetAll(new Lesson { InstructorId = instructorId,SportId = sportId, Day = day});

            if (result.Count() == 0)
                return NotFound();

            return Ok(result.Select(mapper.Map<LessonData>));
        }

        [HttpGet("{lessonId}/Clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<LessonData>>> GetUsers(string lessonId)
        {

            var result = await repo.GetLessonUsers(lessonId);

            if (result.Count() == 0)
                return NotFound();

            return Ok(result.Select(mapper.Map<UserData>));
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post([FromBody] LessonData lesson)
        {

            await repo.Add(mapper.Map<Lesson>(lesson));

            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Instructor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] LessonData lesson)
        {
            await repo.Update(mapper.Map<Lesson>(lesson));

            return NoContent();
        }
        [HttpDelete("{lessonId}")]
        [Authorize(Roles = "Instructor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string lessonId)
        {
            await repo.Delete(lessonId);

            return NoContent();
        }
    }
}
