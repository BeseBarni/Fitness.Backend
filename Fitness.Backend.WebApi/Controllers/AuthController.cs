using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            
            var result = await _signInManager.PasswordSignInAsync(user.Name, user.Password, true, false);
            if (result.Succeeded) { 
                return Ok(User.Identity.Name);           
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] LoginUser user)
        {
            var result = await _userManager.CreateAsync(new ApplicationUser { UserName = user.Name, Email = "besebarni@gmail.com", EmailConfirmed = true }, user.Password);

            return Ok();
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            return Ok(_userManager.Users);
        }
    }
}
