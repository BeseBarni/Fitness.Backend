using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;
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









        


        public async Task<IEnumerable<Lesson>> GetLessonsAsync(int? cityId, int? sportId, string? instructorId, Day? day)
        {
            return await lessonRepository.GetLessonsAsync(cityId, sportId, instructorId, day);
        }

        public async Task<IEnumerable<Sport>> GetSportsAsync()
        {
            return await lessonRepository.GetSportsAsync();
        }

        public async Task<DbResult> UpdateLessonAsync(Lesson lesson)
        {
            return await lessonRepository.UpdateLessonAsync(lesson);
        }
    }
}
