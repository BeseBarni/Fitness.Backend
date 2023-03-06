using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.BusinessLogic
{
    public class ClientBusinessLogic : IClientBusinessLogic
    {
        private readonly ILessonRepository lessonRepository;

        public ClientBusinessLogic(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        public async Task<DbResult> DeleteLessonAsync(int lessonId)
        {
            return await lessonRepository.DeleteLessonAsync(lessonId);
        }
        
        public async Task<IEnumerable<Lesson>> GetLessonsAsync(string? instructorId,string? clientId, int? cityId, int? sportId, Day? day)
        {
            return await lessonRepository.GetLessonsAsync(instructorId,clientId,cityId,sportId,day);
        }

        public async Task<IEnumerable<Sport>> GetSportsAsync()
        {
            return await lessonRepository.GetSportsAsync();
        }

        public async Task<DbResult> UpdateLessonAsync(Lesson lesson)
        {
            return await lessonRepository.UpdateLessonAsync(lesson);
        }
        public async Task<DbResult> CreateSportAsync(Sport sport)
        {
            return await lessonRepository.CreateSportAsync(sport);
        }
        public async Task<DbResult> CreateLessonAsync(Lesson lesson)
        {
            return await lessonRepository.CreateLessonAsync(lesson);
        }

        public async Task<DbResult> DeleteSportAsync(int sportId)
        {
            return await lessonRepository.DeleteSportAsync(sportId);
        }

        public async Task<DbResult> UpdateSportAsync(Sport sport)
        {
            return await lessonRepository.UpdateSportAsync(sport);
        }
    }
}
