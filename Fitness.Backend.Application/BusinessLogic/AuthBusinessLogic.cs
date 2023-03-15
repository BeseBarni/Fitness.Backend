using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.Contracts.Services;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.Domain.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly AuthDbContext authContext;

        public AuthBusinessLogic(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAuthTokenService tokenService, IInstructorRepository instructorRepo, IUserRepository userRepo, AuthDbContext authContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
            this.instructorRepo = instructorRepo;
            this.userRepo = userRepo;
            this.authContext = authContext;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await userManager.Users.ToListAsync();
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


        public async Task<UserIdData> CheckEmail(string email)
        {
            var authUser = await userManager.FindByEmailAsync(email);

            if (authUser == null)
                throw new ResourceNotFoundException();

            return new UserIdData { Id = authUser.Id };
        }
        public async Task<string> Register(RegisterUser user)
        {
            var u = new ApplicationUser
            {
                UserName = user.Name,
                Email = user.Email,
                EmailConfirmed = true,
                NormalizedEmail = user.Email.ToUpper(),
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(u, user.Password);
            u.PasswordHash = hashed;

            var userStore = new UserStore<ApplicationUser>(authContext);
            await userStore.CreateAsync(u);

            string role = user.IsInstructor ? "Instructor" : "Client";
            await userManager.AddToRoleAsync(u, role);

            await userRepo.Add(new User { Id = u.Id, Name = u.UserName, Gender = user.Gender });
            if (user.IsInstructor)
                await instructorRepo.Add(new Instructor { UserId = u.Id, Status = InstructorStatus.ACCEPTED });
            return await Login(new LoginUser { Email = user.Email, Password = user.Password });
        }
        
        public async Task RegisterWithoutLogin(RegisterUser user)
        {
            var u = new ApplicationUser
            {
                UserName = user.Name,
                Email = user.Email,
                EmailConfirmed = true,
                NormalizedEmail = user.Email.ToUpper(),
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(u, user.Password);
            u.PasswordHash = hashed;

            var userStore = new UserStore<ApplicationUser>(authContext);
            await userStore.CreateAsync(u);
            await authContext.SaveChangesAsync();

            string role = user.IsInstructor ? "Instructor" : "Client";
            await userManager.AddToRoleAsync(u, role);

            await userRepo.Add(new User { Id = u.Id, Name = u.UserName, Gender = user.Gender });
            if (user.IsInstructor)
                await instructorRepo.Add(new Instructor { UserId = u.Id, Status = InstructorStatus.ACCEPTED });
        }

    }
}
