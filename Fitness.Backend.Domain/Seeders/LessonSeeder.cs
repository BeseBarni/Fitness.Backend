using Fitness.Backend.Application.Contracts.Repositories;
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
using static System.Net.Mime.MediaTypeNames;

namespace Fitness.Backend.Domain.Seeders
{
    public class LessonSeeder
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository userRepo;
        public LessonSeeder(AppDbContext context, UserManager<ApplicationUser> userManager, IUserRepository userRepo)
        {
            _context = context;
            _userManager = userManager;
            this.userRepo = userRepo;
        }

        public async Task Initialize()
        {
            if (_context.Sports.Any())
                return;

            var r = new Random();

            List<Sport> sports = new List<Sport>
            {
                new Sport { Name = "Atlétika" },
                new Sport { Name = "Kosárlabda" },
                new Sport { Name = "Úszás" },
                new Sport { Name = "Futás" },
                new Sport { Name = "Tenisz" },
                new Sport { Name = "Sakk" },
                new Sport { Name = "Torna" },
                new Sport { Name = "Vívás" },
                new Sport { Name = "Röplabda" },
                new Sport { Name = "Judo" },
                new Sport { Name = "Karate" },
                new Sport { Name = "Úszás" },
                new Sport { Name = "Foci" },
                new Sport { Name = "Ritmus sportgimnasztika" },
                new Sport { Name = "Asztalitenisz" },
                new Sport { Name = "Evezés" },
                new Sport { Name = "Kajak-kenu" },
                new Sport { Name = "Vízilabda" },
                new Sport { Name = "Golf" },
                new Sport { Name = "Síelés" }
            };

            _context.Sports.AddRange(sports);
            await _context.SaveChangesAsync();
            List<Lesson> lessons = new List<Lesson>();
            foreach (var instructor in _context.Instructors)
            {
                for (int i = 0; i < 6; i++)
                {
                    instructor.Sports = new List<Sport>();
                    
                    instructor.Sports.Add(sports[r.Next(0,sports.Count())]);
                    lessons.Add(new Lesson
                    {
                        Instructor = instructor,
                        Sport = instructor.Sports.First(),
                        MaxNumber = r.Next(10, 30),
                        Day = (Day)i,
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddMinutes(50),
                        Name = string.Format("{0} {1} órája", instructor.User.Name, instructor.Sports.First().Name),
                        Users = new List<User>()
                    });

               } 
            }



            foreach (var lesson in lessons)
            {
                for (int i = 0; i < r.Next(0,(int)lesson.MaxNumber); i++)
                {
                    var skip = (int)r.Next(0, _context.Clients.Count());
                    var randUser = await _context.Clients.Skip(skip).Take(1).FirstOrDefaultAsync();
                    if (!lesson.Users.Contains(randUser))
                        lesson.Users.Add(randUser);
                }
            }

            _context.Lessons.AddRange(lessons);

            HttpClient client = new HttpClient();
            foreach (var user in _context.Clients)
            {
                Stream data;
                if(user.Gender == Gender.MALE)
                    data = await client.GetStreamAsync(@"https://xsgames.co/randomusers/avatar.php?g=male");
                else
                    data = await client.GetStreamAsync(@"https://xsgames.co/randomusers/avatar.php?g=female");


                var img = new Application.DataContracts.Models.Entity.Image
                {
                    Name = string.Format("{0}_profile", user.Id),
                    ContentType = "image/jpg"
                };

                MemoryStream memoryStream = new MemoryStream();
                await data.CopyToAsync(memoryStream);
                img.ImageData = memoryStream.ToArray();
                user.Image = img;
            }

            await _context.SaveChangesAsync();
        }

    }
}
