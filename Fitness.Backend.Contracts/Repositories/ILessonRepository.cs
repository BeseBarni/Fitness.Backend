using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.Repositories
{
    public interface ILessonRepository
    {
        public Task<IEnumerable<Lesson>> GetLessonsAsync(string instructorId);
        public Task<Lesson?> GetLessonAsync(int lessonId);
        public Task<DbResult> DeleteLessonAsync(int lessonId);
        public Task<DbResult> UpdateLessonAsync(Lesson lesson);
        public Task<IEnumerable<Sport>> GetSportsAsync();

        Task<IEnumerable<Lesson>> GetLessonsAsync(int cityId);
        Task<IEnumerable<Lesson>> GetLessonsAsync(Sport sport);
        Task<IEnumerable<Lesson>> GetLessonsAsync(string instructorId, int sportId);
        Task<IEnumerable<Lesson>> GetLessonsAsync(int cityId, int sportId);



    }
}
