using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.Contracts.Services;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Domain.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fitness.Backend.Application.BusinessLogic
{
    public class AuthBusinessLogic : IAuthBusinessLogic
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAuthTokenService tokenService;
        private readonly IInstructorRepository instructorRepo;
        private readonly IUserRepository userRepo;
        private readonly IUserBusinessLogic userBl;
        private readonly AuthDbContext authContext;

        public AuthBusinessLogic(IUserBusinessLogic userBl,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAuthTokenService tokenService, IInstructorRepository instructorRepo, IUserRepository userRepo, AuthDbContext authContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
            this.instructorRepo = instructorRepo;
            this.userRepo = userRepo;
            this.authContext = authContext;
            this.userBl = userBl;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<LoggedInUserData> Login(LoginUser user)
        {
            var authUser = await userManager.FindByEmailAsync(user.Email);

            if (authUser is null)
                throw new ResourceNotFoundException();
            if (authUser.Email is null)
                throw new ResourceNotFoundException();

            var result = await userManager.CheckPasswordAsync(authUser, user.Password);

            if (result)
            {

                var userRoles = await userManager.GetRolesAsync(authUser);



                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, authUser.Name),
                    new Claim("emailConfirmed", authUser.EmailConfirmed.ToString())
                };
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                if (userRoles.Contains("Instructor"))
                {
                    var inst = await instructorRepo.GetOne(authUser.Id);

                    authClaims.Add(new Claim("instructorStatus", inst.Status.ToString()));

                }

                var token = tokenService.GenerateToken(authClaims);

                var loginUser = await userRepo.GetOne(authUser.Id);
                return new LoggedInUserData { JwtToken = new JwtSecurityTokenHandler().WriteToken(token), Email = loginUser.Email,
                 Gender = loginUser.Gender, Id = loginUser.Id, ImageId = loginUser.ImageId, Name = loginUser.Name};
            }
            throw new UnauthorizedException();
        }


        public async Task<UserIdData> CheckEmail(string email)
        {
            var authUser = await userManager.FindByEmailAsync(email);

            if (authUser == null)
                throw new ResourceNotFoundException();

            return new UserIdData(authUser.Id);
        }
        public async Task<LoggedInUserData> Register(RegisterUser user)
        {

            await RegisterWithoutLogin(user, true);
            return await Login(new LoginUser { Email = user.Email, Password = user.Password });
        }
        
        public async Task RegisterWithoutLogin(RegisterUser user, bool instructorPending = false)
        {
            var authUser = await userManager.FindByEmailAsync(user.Email);

            if (authUser is not null)
                throw new ResourceAlreadyExistsException(user.Email);

            var u = new ApplicationUser
            {
                Name = user.Name,
                UserName = user.Email,
                NormalizedUserName = user.Email.ToUpper(),
                Email = user.Email,
                EmailConfirmed = true,
                NormalizedEmail = user.Email.ToUpper(),
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(u, user.Password);
            u.PasswordHash = hashed;

            await userManager.CreateAsync(u);
            
            string role = user.IsInstructor ? "Instructor" : "Client";
            await userManager.AddToRoleAsync(u, role);

            await userRepo.Add(new User { Id = u.Id, Name = u.Name, Gender = user.Gender, Email = user.Email });
            var status = instructorPending ? InstructorStatus.VALIDATION_PENDING : InstructorStatus.ACCEPTED;
            if (user.IsInstructor)
                await instructorRepo.Add(new Instructor { UserId = u.Id, Id = u.Id, Status = status });
        }

    }
}
