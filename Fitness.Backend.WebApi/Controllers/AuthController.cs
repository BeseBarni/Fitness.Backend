﻿using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.DataContracts.Models.Entity.DatabaseEntities;
using Fitness.Backend.Application.DataContracts.Models.ViewModels;
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
        public async Task<ActionResult<JwtToken>> Login([FromBody] LoginUser user)
        {

            var result = await authBl.Login(user);
            return Ok(new JwtToken { Value = result});
        }

        [HttpPost]
        public async Task<ActionResult<UserIdData>> CheckEmail([FromBody] CheckEmailData email)
        {

            var result = await authBl.CheckEmail(email.Email);
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
