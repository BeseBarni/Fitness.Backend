using AutoMapper;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Backend.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository repo;
        private readonly IMapper mapper;

        public InstructorController(IInstructorRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<InstructorData>>> Get()
        {

            var result = await repo.GetAll(null);

            if (result.Count() == 0)
                return NotFound();

            return Ok(result.Select(mapper.Map<InstructorData>));
        }

        [HttpPost]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post([FromBody] InstructorData instructor)
        {

            await repo.Add(mapper.Map<Instructor>(instructor));
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Instructor,Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] InstructorData instructor)
        {
            await repo.Update(mapper.Map<Instructor>(instructor));

            return NoContent();
        }

        [HttpPut("{instructorId}/Sports/{sportId}")]
        [Authorize(Roles = "Instructor,Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(string instructorId, string sportId)
        {
            await repo.AddSport(instructorId, sportId);

            return NoContent();
        }


        [HttpGet("{instructorId}/Sports")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetSports(string instructorId)
        {
            var result = await repo.GetSports(instructorId);

            return Ok(mapper.Map<SportData>(result));
        }

        [HttpGet("{instructorId}/Lessons")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetLessons(string instructorId)
        {
            var result = await repo.GetLessons(instructorId);

            return Ok(mapper.Map<LessonData>(result));
        }

        [HttpDelete("{instructorId}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string instructorId)
        {
            await repo.Delete(instructorId);

            return NoContent();
        }
    }
}
