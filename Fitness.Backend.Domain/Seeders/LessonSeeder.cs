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
            if (_context.Sports.Any())
                return;


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

            List<User> instructorUsers = new List<User>
        {
            new User { Name = "Kovács Péter" },
            new User { Name = "Nagy Anna" },
            new User { Name = "Szabó Máté" },
            new User { Name = "Tóth Zsófia" },
            new User { Name = "Horváth Ádám" },
            new User { Name = "Kis Petra" },
            new User { Name = "Balogh Bence" },
            new User { Name = "Farkas Csaba" },
            new User { Name = "Molnár Zoltán" },
            new User { Name = "Takács Eszter" }
        };
            _context.Clients.AddRange(instructorUsers);
            List<Instructor> instructors = new List<Instructor>();
            Random random = new Random();

            foreach (var user in instructorUsers)
            {
                var ins = new Instructor
                {
                    User = user,
                    Sports = new List<Sport>(),
                    Status = InstructorStatus.ACCEPTED
                    
                };
                ins.Sports.Add(sports[random.Next(sports.Count)]);
                ins.Description = $"Üdvözlöm, én vagyok {user.Name}, az oktatója a {ins.Sports.First().Name} sportnak!";
                instructors.Add(ins);

            }
            _context.Instructors.AddRange(instructors);
            List<Lesson> lessons = new List<Lesson>();

            foreach (var instructor in instructors)
            {
                for (int i = 0; i < 6; i++)
                {
                    lessons.Add(new Lesson
                    {
                        Instructor = instructor,
                        Sport = instructor.Sports.First(),
                        MaxNumber = random.Next(10,30),
                        Day = (Day)i,
                        StartTime = DateTime.UtcNow,
                        EndTime= DateTime.UtcNow.AddMinutes(50),
                        Name = string.Format("{0} {1} órája",instructor.User.Name, instructor.Sports.First().Name),
                        Users = new List<User>()
                    });

               } 
            }


            List<string> firstNames = new List<string>
        {
            "László", "Gábor", "András", "Károly", "Tamás", "Péter", "Béla", "József", "Sándor", "Mihály",
            "Ágnes", "Anikó", "Erzsébet", "Katalin", "Mária", "Judit", "Ildikó", "Erika", "Julianna", "Réka",
            "Zsolt", "Viktor", "Ferenc", "Attila", "Zoltán", "Barnabás", "Dániel", "Levente", "Gergely", "Bence",
            "Anna", "Klára", "Júlia", "Nóra", "Dóra", "Lili", "Krisztina", "Izabella", "Adél", "Irina"
        };

            List<string> lastNames = new List<string>
        {
            "Kovács", "Tóth", "Nagy", "Szabó", "Horváth", "Kiss", "Farkas", "Varga", "Balogh", "Molnár",
            "Sipos", "Király", "Bakos", "Papp", "Csizmadia", "Kocsis", "Juhász", "Mészáros", "Simon", "Takács",
            "Novák", "Kovácsné", "Tóthné", "Nagyné", "Szabóné", "Horváthné", "Kissné", "Farkasné", "Vargáné", "Baloghné",
            "Molnárné", "Siposné", "Királyné", "Bakosné", "Pappné", "Csizmadia", "Kocsisné", "Juhászné", "Mészárosné", "Simonné"
        };

            List<User> users = new List<User>();

            for (int i = 0; i < 40; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Count)];
                string lastName = lastNames[random.Next(lastNames.Count)];
                string fullName = $"{lastName} {firstName}";

                users.Add(new User { Name = fullName });
            }
            _context.Clients.AddRange(users);
            foreach (var lesson in lessons)
            {
                for (int i = 0; i < random.Next(0,(int)lesson.MaxNumber); i++)
                {
                    var randUser = users[random.Next(0, users.Count() - 1)];
                    if (!lesson.Users.Contains(randUser))
                        lesson.Users.Add(randUser);
                }
            }

            _context.Lessons.AddRange(lessons);

            await _context.SaveChangesAsync();
        }

    }
}
