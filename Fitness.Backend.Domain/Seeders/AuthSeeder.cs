using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.Backend.Domain.DbContexts;

namespace Fitness.Backend.Domain.Seeders
{
    public class AuthSeeder
    {
        private readonly AuthDbContext _context;
        UserManager<ApplicationUser> _userManager;

        public AuthSeeder(AuthDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Initialize()
        {

            string[] roles = new string[] { "Administrator", "Instructor", "Client" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(_context);

                if (!_context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role) { NormalizedName = role.ToUpper()});
                }
            }


            var users = new List<ApplicationUser>()
            { 
                new ApplicationUser
                {

                    Email = "fitness.admin@backend.com",
                    NormalizedEmail = "FITNESS.ADMIN@BACKEND.COM",
                    UserName = "Administrator",
                    NormalizedUserName = "ADMINISTRATOR",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                new ApplicationUser
                {

                    Email = "fitness.instructor@backend.com",
                    NormalizedEmail = "FITNESS.INSTRUCTOR@BACKEND.COM",
                    UserName = "Instructor",
                    NormalizedUserName = "INSTRUCTOR",
                    PhoneNumber = "+222222222222",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                                                new ApplicationUser
                {

                    Email = "fitness.client@backend.com",
                    NormalizedEmail = "FITNESS.CLIENT@BACKEND.COM",
                    UserName = "Client",
                    NormalizedUserName = "CLIENT",
                    PhoneNumber = "+333333333333",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }
            };
            int i = 0;
            foreach (var user in users)
            {
                if (!_context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, "fitness");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(_context);
                    var result = await userStore.CreateAsync(user);
                    i++;
                }
                
                await AssignRoles(user.Email, roles[i]);
            }


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
