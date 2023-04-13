using AutoMapper;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fitness.Backend.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IUserBusinessLogic bl;
        private readonly IMapper mapper;

        public ClientController(IUserBusinessLogic bl, IMapper mapper)
        {
            this.bl = bl;
            this.mapper = mapper;
        }

        [HttpGet("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserData>> Get(string clientId)
        {
            var result = await bl.GetOne(clientId);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserData>>> Get()
        {
            var result = await bl.GetAll(null);

            return Ok(result);
        }

        [HttpGet("{clientId}/Lessons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonData>>> GetLessons(string clientId)
        {
            var result = await bl.GetLessons(clientId);
            return Ok(result);
        }

        [HttpGet("{clientId}/Picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPicture(string clientId)
        {
            var result = await bl.GetImage(clientId);

            return File(result.ImageData, result.ContentType, result.Name);
        }

        [HttpPost("{clientId}/Picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonData>>> AddPicture(string clientId, IFormFile image)
        {
            await bl.AddImage(clientId, image);
            return NoContent();
        }


        [HttpPut("{clientId}/Lessons/{lessonId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<LessonData>>> AddLesson(string clientId, string lessonId)
        {
            await bl.AddLesson(lessonId, clientId);

            return NoContent();
        }

        [HttpDelete("{clientId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string clientId)
        {
            await bl.Delete(clientId);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] UserData user)
        {

            await bl.Update(user);
            return NoContent();
        }

    }
}
