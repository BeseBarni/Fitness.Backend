using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.Domain.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Domain.Seeders
{
    public class LessonSeeder
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public LessonSeeder(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Initialize()
        {
            _context.Database.Migrate();

            if (_context.Sports.Count() != 0)
                return;
            var sports = new string[]{ "Karate", "Yoga", "Spinning", "Boying", "Running", "Linux szerver telepítés", "Vim-ből kilépés" };
            if (_context.Sports.Count() != 0)
                return;
            foreach (var sport in sports)
            {
                    await _context.Sports.AddAsync(new Sport { Name = sport});

            }
            await _context.SaveChangesAsync();


            if (_context.Clients.Count() != 0)
                return;
            var identityUser = await _userManager.FindByEmailAsync("fitness.instructor@backend.com");
            var user = new User()
            {
                Id = identityUser.Id,
                Name = "Har Mónika"
            };
            var instructor = new Instructor()
            {
                Id = "1",
                User= user,
                Description = "Sziasztok, Har Mónikának hívnak",
                Status = InstructorStatus.ACCEPTED,
                Sports = _context.Sports.Where(p => p.Name == "Karate" || p.Name == "Yoga").ToList()
            };
            _context.Instructors.Add(instructor);

            await _context.SaveChangesAsync();

            identityUser = await _userManager.FindByEmailAsync("fitness.client@backend.com");
            var client = new User()
            {
                Id = identityUser.Id,
                Name = "Cserepes Virág",
                Lessons= new List<Lesson>()
                 {
                     new Lesson
                     {
                           Instructor = instructor,
                           Location = "109-es terem",
                            MaxNumber = 10,
                             Day = Day.FRIDAY,
                              StartTime= DateTime.UtcNow,
                              EndTime= DateTime.UtcNow.AddMinutes(120),
                               Name = "USB Type C foglalat hajtogatása",
                               Sport = _context.Sports.FirstOrDefault(p => p.Name == "Linux szerver telepítés")
                                 
                     }
                 }
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
    }
}
