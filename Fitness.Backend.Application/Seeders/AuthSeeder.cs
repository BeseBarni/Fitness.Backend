using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Fitness.Backend.Domain.DbContexts;
using Microsoft.EntityFrameworkCore;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Application.DataContracts.Entity;

namespace Fitness.Backend.Application.Seeders
{
    /// <summary>
    /// initializes the Indentity Auth database with users
    /// </summary>
    public class AuthSeeder
    {
        private readonly AuthDbContext _context;
        private readonly AppDbContext appContext;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthBusinessLogic authBl;
        private readonly IUserRepository userRepo;

        public AuthSeeder(AuthDbContext context, UserManager<ApplicationUser> userManager, IAuthBusinessLogic authBl, IUserRepository userRepo, AppDbContext appContext)
        {
            _context = context;
            _userManager = userManager;
            this.authBl = authBl;
            this.userRepo = userRepo;
            this.appContext = appContext;
        }

        public async Task Initialize()
        {
            _context.Database.Migrate();
            appContext.Database.Migrate();

            string[] roles = new string[] { "Administrator", "Instructor", "Client" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(_context);

                if (!_context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role) { NormalizedName = role.ToUpper() });
                }
            }

            if (_userManager.Users.Any())
                return;


            var r = new Random();
            List<string> maleFirstNames = new List<string>
            {
                "Ábel",
                "Ádám",
                "Ákos",
                "Albert",
                "András",
                "Antal",
                "Attila",
                "Balázs",
                "Béla",
                "Botond",
                "Csaba",
                "Dániel",
                "Gábor",
                "István",
                "János",
                "László",
                "Levente",
                "Máté",
                "Miklós",
                "Norbert",
                "Péter",
                "Sándor",
                "Tamás",
                "Viktor",
                "Zoltán"
            };

            List<string> femaleFirst = new List<string>() {
                "Anna", "Zsófia", "Eszter", "Katalin", "Ágnes", "Mária", "Judit", "Julianna", "Borbála",
                "Viktória", "Lilla", "Emília", "Henrietta", "Éva", "Kitti", "Orsolya", "Szilvia", "Boglárka",
                "Gabriella", "Renáta", "Dóra", "Flóra", "Noémi", "Enikő", "Melinda", "Réka", "Zsuzsanna",
                "Ibolya", "Mónika", "Bernadett", "Zsanett", "Margit"
            };

            var registerUsers = new List<RegisterUser>()
            {
                new RegisterUser { Email = "fitness.instructor@backend.com", Gender = Gender.MALE, IsInstructor = true, Name = "Instructor Pista", Password = "fitness" },
                new RegisterUser { Email = "fitness.client@backend.com", Gender = Gender.MALE, IsInstructor = false, Name = "Client Béla", Password = "fitness" }
            };

            for (int i = 0; i < 15; i++)
            {
                registerUsers.Add(new RegisterUser
                {
                    Email = "fitness.client" + i + "@backend.com",
                    Name = "Client" + " " + maleFirstNames[r.Next(0, maleFirstNames.Count())],
                    Password = "fitness",
                    Gender = Gender.MALE,
                    IsInstructor = false,
                });
                registerUsers.Add(new RegisterUser
                {
                    Email = "fitness.client" + (i + 15) + "@backend.com",
                    Name = "Client" + " " + femaleFirst[r.Next(0, femaleFirst.Count())],
                    Password = "fitness",
                    Gender = Gender.FEMALE,
                    IsInstructor = false,
                });
            };
            for (int i = 0; i < 10; i++)
            {
                int gender = r.Next(0, 2);
                registerUsers.Add(new RegisterUser
                {
                    Email = "fitness.instructor" + (i + 1) + "@backend.com",
                    Name = "Client" + " " + (gender == 0 ? maleFirstNames[r.Next(0, maleFirstNames.Count())] : femaleFirst[r.Next(0, femaleFirst.Count())]),
                    Password = "fitness",
                    Gender = (Gender)gender,
                    IsInstructor = true,
                });
            }

            foreach (var user in registerUsers)
            {
                await authBl.RegisterWithoutLogin(user);

            }

            var u = new ApplicationUser
            {
                UserName = "Admin Pisti",
                Email = "fitness.admin@backend.com",
                EmailConfirmed = true,
                NormalizedEmail = "fitness.admin@backend.com".ToUpper()
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(u, "fitness");
            u.PasswordHash = hashed;

            var userStore = new UserStore<ApplicationUser>(_context);
            await userStore.CreateAsync(u);

            await _userManager.AddToRoleAsync(u, roles[0]);

            await userRepo.Add(new User { Id = u.Id, Name = u.UserName, Gender = Gender.MALE, Email = u.Email });

            await _context.SaveChangesAsync();
        }

        public async Task<IdentityResult> AssignRoles(string email, string role)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRoleAsync(user, role);

            return result;
        }
    }
}
