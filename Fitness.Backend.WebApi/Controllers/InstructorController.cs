using AutoMapper;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Backend.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorBusinessLogic bl;

        public InstructorController(IInstructorBusinessLogic bl)
        {
            this.bl = bl;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<InstructorData>>> Get()
        {

            var result = await bl.GetAll(null);

            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post([FromBody] InstructorData instructor)
        {

            await bl.Add(instructor);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Instructor,Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] InstructorData instructor)
        {
            await bl.Update(instructor);

            return NoContent();
        }

        [HttpPut("{instructorId}/Sports/{sportId}")]
        [Authorize(Roles = "Instructor,Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(string instructorId, string sportId)
        {
            await bl.AddSport(instructorId, sportId);

            return NoContent();
        }


        [HttpGet("{instructorId}/Sports")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetSports(string instructorId)
        {
            var result = await bl.GetSports(instructorId);

            return Ok(result);
        }

        [HttpGet("{instructorId}/Lessons")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetLessons(string instructorId)
        {
            var result = await bl.GetLessons(instructorId);

            return Ok(result);
        }

        [HttpDelete("{instructorId}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string instructorId)
        {
            await bl.Delete(instructorId);

            return NoContent();
        }
    }
}
