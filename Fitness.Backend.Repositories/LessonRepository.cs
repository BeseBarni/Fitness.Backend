using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Domain.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        //todo EF core db, db context implementation
        private readonly AppDbContext context;

        public LessonRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync(int? cityId, int? sportId, string? instructorId, Day? day)
        {
            return await context.Lessons.Where(p => (cityId == null || p.CityId == cityId) && 
            (sportId == null || p.Sport.Id == sportId) &&
            (instructorId == null || p.InstructorId == instructorId) &&
            (day == null || p.Day == day)).ToListAsync();
        }



        public async Task<DbResult> DeleteLessonAsync(int lessonId)
        {
            var lesson = await context.Lessons.FirstOrDefaultAsync(p => p.Id == lessonId);
            if (lesson == null)
                return DbResult.NOT_FOUND;

            context.Lessons.Remove(lesson);
            try
            {
                await context.SaveChangesAsync();
                return DbResult.DELETED;
            }
            catch (Exception)
            {

                return DbResult.FAILED;
            }
        }

        public async Task<DbResult> DeleteSportAsync(int sportId)
        {
            var sport = await context.Sports.FirstOrDefaultAsync(p => p.Id == sportId);
            if (sport == null)
                return DbResult.NOT_FOUND;

            context.Lessons.Remove(sport);
            try
            {
                await context.SaveChangesAsync();
                return DbResult.DELETED;
            }
            catch (Exception)
            {

                return DbResult.FAILED;
            }
        }

        public async Task<DbResult> UpdateLessonAsync(Lesson lesson)
        {
            var entity = await context.Lessons.FirstOrDefaultAsync(p => p.Id == lesson.Id);

            if(entity == null)
                return DbResult.NOT_FOUND;

            context.Lessons.Update(entity);

            try
            {
                await context.SaveChangesAsync();
                return DbResult.UPDATED;
            }
            catch (Exception)
            {

                return DbResult.FAILED;
            }
        }

        public async Task<DbResult> UpdateSportAsync(Sport sport)
        {
            var entity = await context.Sports.FirstOrDefaultAsync(p => p.Id == sport.Id);

            if (entity == null)
                return DbResult.NOT_FOUND;

            context.Sports.Update(entity);

            try
            {
                await context.SaveChangesAsync();
                return DbResult.UPDATED;
            }
            catch (Exception)
            {

                return DbResult.FAILED;
            }
        }


        public async Task<IEnumerable<Sport>> GetSportsAsync()
        {
            return await context.Sports.ToListAsync();
        }

        public async Task<DbResult> CreateSportAsync(Sport sport)
        {
            context.Sports.Add(sport);
            try
            {
                await context.SaveChangesAsync();
                return DbResult.CREATED;
            }
            catch (Exception)
            {
                return DbResult.FAILED;
            }
        }

        public async Task<DbResult> CreateLessonAsync(Lesson lesson)
        {
            context.Lessons.Add(lesson);
            try
            {
                await context.SaveChangesAsync();
                return DbResult.CREATED;
            }
            catch (Exception)
            {
                return DbResult.FAILED;
            }
        }

    }
}
        }

        public async Task<DbResult> CreateUser(User user)
        {
            var entity = await context.Clients.FirstOrDefaultAsync(p => p.Id == user.Id);

            if (entity == null)
                return DbResult.NOT_FOUND;

            context.Clients.Add(user);

            try
            {
                await context.SaveChangesAsync();
                return DbResult.CREATED;
            }
            catch (Exception)
            {
                return DbResult.FAILED;
            }
        }

        public async Task<DbResult> CreateInstructor(Instructor instructor)
        {
            var client = await context.Clients.FirstOrDefaultAsync(p => p.Id == instructor.UserId);

            var entity = await context.Instructors.FirstOrDefaultAsync(p => p.UserId == instructor.UserId);

            if (entity == null || client == null)
                return DbResult.NOT_FOUND;

            context.Instructors.Add(instructor);

            try
            {
                await context.SaveChangesAsync();
                return DbResult.CREATED;
            }
            catch (Exception)
            {
                return DbResult.FAILED;
            }
        }

        public async Task<DbResult> AddLessonToClient(int lessonId, string clientId)
        {
            var entity = await context.Clients.FirstOrDefaultAsync(p => p.Id == clientId);
            var lesson = await context.Lessons.FirstOrDefaultAsync(p => p.Id == lessonId);

            if (entity == null || lesson == null)
                return DbResult.NOT_FOUND;

            entity.Lessons.Add(lesson);          

            try
            {
                await context.SaveChangesAsync();
                return DbResult.CREATED;
            }
            catch (Exception)
            {
                return DbResult.FAILED;
            }
        }

        public async Task<IEnumerable<Lesson>> GetClientLessons(string clientId)
        {
            var entity = await context.Clients.FirstOrDefaultAsync(p => p.Id == clientId);
            return entity.Lessons;
        }

    }
}
