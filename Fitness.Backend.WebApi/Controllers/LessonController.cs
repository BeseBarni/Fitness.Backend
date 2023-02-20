using Fitness.Backend.Application.Contracts.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IClientBusinessLogic clientBl;

        public LessonController(IClientBusinessLogic clientBl)
        {
            this.clientBl = clientBl;
        }

        [HttpGet]
        public async Task<IActionResult> GetLessonListByInstructor(string instructorId)
        {
            var result = await clientBl.GetLessonListByInstructorAsync(instructorId);
            return Ok(result);
        }
    }
}
