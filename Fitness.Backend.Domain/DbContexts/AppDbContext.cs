using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Domain.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var cities = new List<City>
            {
                new City { PostalCode = 1011, Name = "Budapest", Id = 1 },
                new City { PostalCode = 4024, Name = "Debrecen", Id = 2 },
                new City { PostalCode = 3525, Name = "Miskolc", Id = 3 },
                new City { PostalCode = 6720, Name = "Szeged", Id = 4 },
                new City { PostalCode = 7621, Name = "Pécs", Id = 5 },
                new City { PostalCode = 9021, Name = "Győr", Id = 6 },
                new City { PostalCode = 4400, Name = "Nyíregyháza", Id = 7 },
                new City { PostalCode = 6000, Name = "Kecskemét", Id = 8 },
                new City { PostalCode = 8000, Name = "Székesfehérvár", Id = 9 },
                new City { PostalCode = 9700, Name = "Szombathely", Id = 10 },
                new City { PostalCode = 5000, Name = "Szolnok", Id = 11 },
                new City { PostalCode = 2800, Name = "Tatabánya", Id = 12 },
                new City { PostalCode = 7400, Name = "Kaposvár", Id = 13 },
                new City { PostalCode = 2030, Name = "Érd", Id = 14 },
                new City { PostalCode = 8200, Name = "Veszprém", Id = 15 },
                new City { PostalCode = 5600, Name = "Békéscsaba", Id = 16 },
                new City { PostalCode = 8900, Name = "Zalaegerszeg", Id = 17 },
                new City { PostalCode = 9400, Name = "Sopron", Id = 18 },
                new City { PostalCode = 3300, Name = "Eger", Id = 19 },
                new City { PostalCode = 8800, Name = "Nagykanizsa", Id = 20 }
            };

            modelBuilder.Entity<City>().HasData(cities);
        }
    }
}
