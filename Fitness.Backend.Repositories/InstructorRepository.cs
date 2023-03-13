using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Application.DataContracts.Extensions;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.Domain.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Repositories
{
    public class InstructorRepository : BaseRepository, IInstructorRepository
    {
        public InstructorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task Add(Instructor parameters)
        {
            if (context.Instructors.DelFilter().Where(p => p.UserId == parameters.UserId).Count() > 0)
                throw new Exception();

            context.Add(parameters);

            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var instructor = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if (instructor == null)
                throw new ResourceNotFoundException();

            instructor.Del = 1;
            context.Entry(instructor)
                .Collection(b => b.Lessons)
                .Load();

            if(instructor.Lessons != null)
                if(instructor.Lessons.Count() > 0)
                    foreach (var lesson in instructor.Lessons)
                        lesson.Del = 1;


            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instructor>> GetAll(Instructor parameters)
        {
            var result = await context.Instructors.DelFilter().ToListAsync();
                

            return result;
        }

        public async Task<Instructor> GetOne(string id)
        {
            var result = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if (result == null)
                throw new ResourceNotFoundException();

            return result;
        }

        public async  Task Update(Instructor parameters)
        {
            var instructor = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == parameters.Id);
            if (instructor == null)
                throw new ResourceNotFoundException();

            instructor.Status = parameters.Status ?? instructor.Status;
            instructor.Description = parameters.Description ?? instructor.Description;

            await context.SaveChangesAsync();
        }

        public async Task AddSport(string instructorId,string sportId)
        {
            var sport = await context.Sports.DelFilter().FirstOrDefaultAsync(p => p.Id == sportId);

            if(sport == null)
                throw new ResourceNotFoundException();

            var instructor = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == instructorId);

            if (instructor == null)
                throw new ResourceNotFoundException();

            instructor.Sports = instructor.Sports ?? new List<Sport>();
            instructor.Sports.Add(sport);
            await context.SaveChangesAsync();
        }
    }
}
