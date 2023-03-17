using Fitness.Backend.Application.DataContracts.Models.Entity.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<User> Clients { get; set; }
        public DbSet<Image> ProfilePictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
