using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Domain.DbContexts;
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

        public async Task<Lesson?> GetLessonAsync(int lessonId)
        {
            return await context.Lessons.Where(p => p.Id == lessonId).FirstOrDefaultAsync();
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

        public async Task<IEnumerable<Lesson>> GetLessonsAsync(string instructorId)
        {
            return await context.Lessons.Where(p => p.InstructorId == instructorId).ToListAsync();
        }

        public async Task<IEnumerable<Sport>> GetSportsAsync()
        {
            return await context.Sports.ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync(int cityId)
        {
            return await context.Lessons.Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync(Sport sport)
        {
            return await context.Lessons.Where(p => p.Sport.Id == sport.Id).ToListAsync();

        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync(string instructorId, int sportId)
        {
            return await context.Lessons.Where(p => p.Sport.Id == sportId && p.InstructorId == instructorId).ToListAsync();

        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync(int cityId, int sportId)
        {
            return await context.Lessons.Where(p => p.Sport.Id == sportId && p.CityId == cityId).ToListAsync();

        }
    }
}
