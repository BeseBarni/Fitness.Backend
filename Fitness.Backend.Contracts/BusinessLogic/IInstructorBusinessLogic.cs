﻿using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.BusinessLogic
{
    public interface IInstructorBusinessLogic : IBaseBusinessLogic<InstructorData>
    {
        Task AddSport(string instructorId, string sportId);
        Task<IEnumerable<SportData>?> GetSports(string instructorId);
        Task<IEnumerable<LessonData>?> GetLessons(string instructorId);
    }
}
