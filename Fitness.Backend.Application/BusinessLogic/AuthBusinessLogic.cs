using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.Contracts.Services;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.BusinessLogic
{
    public class AuthBusinessLogic : IAuthBusinessLogic
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAuthTokenService tokenService;
        private readonly IInstructorRepository instructorRepo;
        private readonly IUserRepository userRepo;

        public AuthBusinessLogic(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAuthTokenService tokenService, IInstructorRepository instructorRepo, IUserRepository userRepo)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
            this.instructorRepo = instructorRepo;
            this.userRepo = userRepo;
        }

        public async Task<string> Login(LoginUser user)
        {
            var authUser = await userManager.FindByEmailAsync(user.Email);

            if (authUser == null)
                throw new ResourceNotFoundException();

            var result = await userManager.CheckPasswordAsync(authUser, user.Password);

            if (result)
            {

                var userRoles = await userManager.GetRolesAsync(authUser);



                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, authUser.UserName),
                    new Claim("emailConfirmed", authUser.EmailConfirmed.ToString())
                };
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                if (userRoles.Contains("Instructor"))
                {
                    var inst = await instructorRepo.GetAll(new Instructor { UserId = authUser.Id });
                    authClaims.Add(new Claim("instructorStatus", inst.First().Status.ToString()));

                }

                var token = tokenService.GenerateToken(authClaims);
                return (new JwtSecurityTokenHandler().WriteToken(token));
            }
            throw new UnauthorizedException();
        }


        public async Task<string> CheckEmail(string email)
        {
            var authUser = await userManager.FindByEmailAsync(email);

            if (authUser == null)
                throw new ResourceNotFoundException();

            return authUser.Id;
        }
        public Task<string> Register(RegisterUser user)
        {
            throw new NotImplementedException();
        }
    }
}
