using AutoMapper;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fitness.Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IUserRepository repo;
        private readonly IMapper mapper;

        public ClientController(IUserRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserData>> Get(string clientId)
        {
            var result = await repo.GetOne(clientId);

            return Ok(mapper.Map<UserData>(result));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserData>>> Get()
        {
            var result = await repo.GetAll(null);

            return Ok(result.Select(mapper.Map<UserData>));
        }

        [HttpGet("{clientId}/Lessons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonData>>> GetLessons(string clientId)
        {
            var result = await repo.GetLessons(clientId);
            return Ok(result.Select(mapper.Map<LessonData>));
        }

        [HttpGet("{clientId}/Picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPicture(string clientId)
        {
            var result = await repo.GetImage(clientId);

            return File(result.ImageData, result.ContentType, result.Name);
        }

        [HttpPost("{clientId}/Picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonData>>> AddPicture(string clientId, IFormFile image)
        {
            await repo.AddImage(clientId, image);
            return NoContent();
        }


        [HttpPut("{clientId}/Lessons/{lessonId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<LessonData>>> AddLesson(string clientId, string lessonId)
        {
            await repo.AddLesson(lessonId, clientId);

            return NoContent();
        }

        [HttpDelete("{clientId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string clientId)
        {
            await repo.Delete(clientId);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] UserData user)
        {

            await repo.Update(mapper.Map<User>(user));
            return NoContent();
        }

    }
}
