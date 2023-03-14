using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Services;
using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {

            var result = await authBl.Login(user);
            return Ok(new JwtToken { Value = result});
        }

        [HttpPost]
        public async Task<IActionResult> CheckEmail([FromBody] string email)
        {

            var result = await authBl.CheckEmail(email);
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
