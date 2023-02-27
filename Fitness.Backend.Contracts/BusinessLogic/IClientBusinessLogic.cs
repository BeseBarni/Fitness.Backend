using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.BusinessLogic
{
    public interface IClientBusinessLogic
    {
        Task<IEnumerable<Lesson>> GetLessonsAsync(string instructorId);
        Task<Lesson?> GetLessonAsync(int lessonId);
        Task<IEnumerable<Sport>> GetSportsAsync();
        Task<DbResult> UpdateLessonAsync(Lesson lesson);
        Task<DbResult> DeleteLessonAsync(int lessonId);

        Task<IEnumerable<Lesson>> GetLessonsAsync(int cityId);
        Task<IEnumerable<Lesson>> GetLessonsAsync(Sport sport);
        Task<IEnumerable<Lesson>> GetLessonsAsync(string instructorId, int sportId);
        Task<IEnumerable<Lesson>> GetLessonsAsync(int cityId, int sportId);
    }
}
