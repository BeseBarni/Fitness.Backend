﻿using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Domain.DbContexts;
using Fitness.Backend.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Backend.Repositories
{
    /// <summary>
    /// Repository for accessing, updating and deleting lesson related data in the database
    /// </summary>
    public class LessonRepository : BaseRepository, ILessonRepository
    {
        public LessonRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<User>?> GetLessonUsers(string lessonId)
        {
            var result = await context.Lessons.Include(l => l.Users).DelFilter().FirstOrDefaultAsync(p => p.Id == lessonId);
            if (result == null)
                throw new ResourceNotFoundException(string.Format("lesson:{0}", lessonId));

            
            return result.Users.Select(p => new User
            {
                Id =p.Id,
                Name = p.Name,
                Email = p.Email,
                Gender =p.Gender,
                ImageId = p.ImageId
            });
        }

        public async Task<IEnumerable<Lesson>?> GetAll(Lesson? parameters)
        {
            return await context.Lessons.Where(p =>
                (parameters.SportId == null || p.SportId == parameters.SportId) &&
                (parameters.InstructorId == null || p.InstructorId == parameters.InstructorId) &&
                (parameters.Day == null || p.Day == parameters.Day)).DelFilter().ToListAsync();
        }

        public async Task Delete(string id)
        {
            var lesson = await context.Lessons.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if (lesson == null)
                throw new ResourceNotFoundException(string.Format("lesson:{0}", id));

            lesson.Del = 1;
            await context.SaveChangesAsync();
        }

        public async Task Update(Lesson parameters)
        {
            var lesson = await context.Lessons.DelFilter().FirstOrDefaultAsync(p => p.Id == parameters.Id);
            if (lesson == null)
                throw new ResourceNotFoundException(string.Format("lesson:{0}", parameters.Id));

            lesson.Name = parameters.Name ?? lesson.Name;
            lesson.Location = parameters.Location ?? lesson.Location;
            lesson.Day = parameters.Day ?? lesson.Day;
            lesson.MaxNumber = parameters.MaxNumber ?? lesson.MaxNumber;
            if(parameters.InstructorId != null)
            {
                var instructor = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == parameters.InstructorId);
                lesson.Instructor = instructor;
            }
            if (parameters.SportId != null)
            {
                var sport = await context.Sports.DelFilter().FirstOrDefaultAsync(p => p.Id == parameters.SportId);
                lesson.Sport = sport;
            }

            await context.SaveChangesAsync();
        }

        public async Task<Lesson> GetOne(string id)
        {
            var result = await context.Lessons.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if(result == null)
                throw new ResourceNotFoundException(string.Format("lesson:{0}", id));

            return result;
        }

        public async Task Add(Lesson parameters)
        {
            var instructor = await context.Instructors.DelFilter().FirstOrDefaultAsync(p => p.Id == parameters.InstructorId);
            var sport = await context.Sports.DelFilter().FirstOrDefaultAsync(p => p.Id == parameters.SportId);
            parameters.Instructor = instructor;
            parameters.Sport = sport;
            context.Lessons.Add(parameters);
            await context.SaveChangesAsync();
        }

        public async Task AddClient(string lessonId,string userId)
        {
            var result = await context.Lessons.DelFilter().FirstOrDefaultAsync(p => p.Id == lessonId);
            if (result == null)
                throw new ResourceNotFoundException(string.Format("lesson:{0}", lessonId));

            var user = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == userId);

            if (user == null)
                throw new ResourceNotFoundException(string.Format("user:{0}",userId));
            result.Users = result.Users ?? new List<User>();
            result.Users.Add(user);
            await context.SaveChangesAsync();

        }
    }
}
