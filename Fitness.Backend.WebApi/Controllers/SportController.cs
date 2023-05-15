using AutoMapper;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Fitness.Backend.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {

        private readonly ISportBusinessLogic bl;


        public SportController(ISportBusinessLogic bl)
        {
            this.bl = bl;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SportData>>> Get()
        {
            var result = await bl.GetAll(null);

            return Ok(result);
        }
        [HttpGet("{sportId}/Instructors")]
        public async Task<ActionResult<IEnumerable<SportData>>> GetInstructors(string sportId)
        {
            var result = await bl.GetInstructors(sportId);

            return Ok(result);
        }

        [HttpDelete("{sportId}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string sportId)
        {
            await bl.Delete(sportId);

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Post([FromBody] SportData sport)
        {
            await bl.Add(sport);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] SportData sport)
        {
            await bl.Update(sport);


            return NoContent();
        }

        [HttpPost("{sportId}/Picture")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonData>>> AddPicture(string sportId, IFormFile image)
        {
            await bl.AddImage(sportId, image);
            return NoContent();
        }

        [HttpGet("{sportId}/Picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPicture(string sportId)
        {
            var result = await bl.GetImage(sportId);

            return File(result.ImageData, result.ContentType, result.Name);
        }
    }
}
