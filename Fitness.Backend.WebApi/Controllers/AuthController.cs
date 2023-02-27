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

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAuthTokenService tokenService;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAuthTokenService tokenService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {

            var authUser = await userManager.FindByNameAsync(user.Name);
            if (authUser == null)
                return NotFound();
            var result = await userManager.CheckPasswordAsync(authUser, user.Password);
            if (result) {

                var userRoles = await userManager.GetRolesAsync(authUser);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, authUser.UserName)
                };


                var token = tokenService.GenerateToken(authClaims);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));           
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] LoginUser user)
        {
            var result = await userManager.CreateAsync(new ApplicationUser { UserName = user.Name, Email = user.Email, EmailConfirmed = true }, user.Password);
            var authUser = await userManager.FindByNameAsync(user.Name);
            
            return Ok(result);
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            return Ok(userManager.Users);
        }

        [HttpGet]
        public IActionResult GetUserClaims()
        {
            return Ok(User.Claims);
        }
    }
}
