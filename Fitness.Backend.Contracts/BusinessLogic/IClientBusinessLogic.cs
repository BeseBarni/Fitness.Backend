using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.BusinessLogic
{
    public interface IClientBusinessLogic
    {
        public Task<DbResult> DeleteLessonAsync(int lessonId);
        public Task<DbResult> DeleteSportAsync(int sportId);
        public Task<DbResult> UpdateLessonAsync(Lesson lesson);
        public Task<DbResult> UpdateSportAsync(Sport sport);
        public Task<IEnumerable<Sport>> GetSportsAsync();
        public Task<IEnumerable<Lesson>> GetLessonsAsync(string? instructorId, string? clientId, int? cityId, int? sportId, Day? day);
        public Task<DbResult> CreateSportAsync(Sport sport);
        public Task<DbResult> CreateLessonAsync(Lesson lesson);

    }
}
