﻿using Fitness.Backend.Application.DataContracts.Enums;
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
        Task<IEnumerable<Sport>> GetSportsAsync();
        Task<DbResult> UpdateLessonAsync(Lesson lesson);
        Task<DbResult> DeleteLessonAsync(int lessonId);
        public Task<IEnumerable<Lesson>> GetLessonsAsync(int? cityId, int? sportId, string? instructorId, Day? day);

    }
}
