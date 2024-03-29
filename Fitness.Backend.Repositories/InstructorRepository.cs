﻿using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Domain.DbContexts;
using Fitness.Backend.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Backend.Repositories
{
    /// <summary>
    /// Repository for accessing, updating and deleting instructor related data in the database
    /// </summary>
    public class InstructorRepository : BaseRepository, IInstructorRepository
    {
        public InstructorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task Add(Instructor parameters)
        {
            if (context.Instructors.DelFilter().Where(p => p.UserId == parameters.UserId).Count() > 0)
                throw new ResourceAlreadyExistsException(string.Format("InstructorId.UserId:{0}",parameters.UserId));

            context.Add(parameters);

            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var instructor = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if (instructor == null)
                throw new ResourceNotFoundException(string.Format("InstructorId:{0}", id));

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

        public async Task<IEnumerable<Instructor>?> GetAll(Instructor? parameters)
        {
            var result = await context.Instructors.DelFilter().ToListAsync();
               
            return result;
        }

        public async Task<Instructor> GetOne(string id)
        {
            var result = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if (result == null)
                throw new ResourceNotFoundException(string.Format("InstructorId:{0}", id));

            return result;
        }

        public async  Task Update(Instructor parameters)
        {
            var instructor = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == parameters.Id);
            if (instructor == null)
                throw new ResourceNotFoundException(string.Format("InstructorId:{0}", parameters.Id));

            instructor.Status = parameters.Status ?? instructor.Status;
            instructor.Description = parameters.Description ?? instructor.Description;

            await context.SaveChangesAsync();
        }

        public async Task AddSport(string instructorId,string sportId)
        {
            var sport = await context.Sports.DelFilter().FirstOrDefaultAsync(p => p.Id == sportId);

            if(sport == null)
                throw new ResourceNotFoundException(string.Format("SportId:{0}",sportId));

            var instructor = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == instructorId);

            if (instructor == null)
                throw new ResourceNotFoundException(string.Format("InstructorId:{0}", instructorId));

            instructor.Sports = instructor.Sports ?? new List<Sport>();
            instructor.Sports.Add(sport);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sport>?> GetSports(string instructorId)
        {
            var result = await context.Instructors.Include(i => i.Sports).DelFilter().FirstOrDefaultAsync(p => p.Id == instructorId);
            if (result == null) throw new ResourceNotFoundException();


            return result.Sports;
        }

        public async Task<IEnumerable<Lesson>?> GetLessons(string instructorId)
        {
            var result = await context.Instructors.Include(i => i.Lessons).DelFilter().FirstOrDefaultAsync(p => p.Id == instructorId);
            if (result == null) throw new ResourceNotFoundException();

            return result.Lessons;
        }
    }
}
