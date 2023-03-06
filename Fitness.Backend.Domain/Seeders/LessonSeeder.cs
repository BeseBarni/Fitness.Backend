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
            var sports = new string[]{ "Karate", "Yoga", "Spinning", "Boying", "Running" };
            foreach (var sport in sports)
            {
                if(await _context.Sports.FirstAsync(p => p.Name == sport) == null)
                    await _context.Sports.AddAsync(new Sport { Name = sport});

            }

            await _context.SaveChangesAsync();

            var lessons = new List<Lesson>()
            {
            };
        }
    }
}
