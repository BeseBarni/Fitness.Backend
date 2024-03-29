﻿using Fitness.Backend.Application.DataContracts.Entity;

namespace Fitness.Backend.Application.Contracts.Repositories
{
    public interface IInstructorRepository : IBaseRepository<Instructor>
    {
        Task AddSport(string instructorId, string sportId);
        Task<IEnumerable<Sport>> GetSports(string instructorId);
        Task<IEnumerable<Lesson>> GetLessons(string instructorId);
    }
}
