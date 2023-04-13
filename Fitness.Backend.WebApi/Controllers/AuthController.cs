using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthBusinessLogic authBl;

        public AuthController(IAuthBusinessLogic authBl)
        {
            this.authBl = authBl;
        }

        [HttpPost]
        public async Task<ActionResult<LoggedInUserData>> Login([FromBody] LoginUser user)
        {

            var result = await authBl.Login(user);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LoggedInUserData>> Register([FromBody] RegisterUser user)
        {

            var result = await authBl.Register(user);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> Get()
        {
            var result =  await authBl.GetUsers();
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] LoginUser user)
        //{
        //    var result = await userManager.CreateAsync(new ApplicationUser { UserName = user.Name, Email = user.Email, EmailConfirmed = true }, user.Password);
        //    var authUser = await userManager.FindByNameAsync(user.Name);

        //    return Ok(result);
        //}
    }
}
