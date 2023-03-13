using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.Repositories
{
    public interface ILessonRepository : IBaseRepository<Lesson>
    {
        Task<IEnumerable<User>> GetLessonUsers(string lessonId);
        Task AddClient(string lessonId, string userId);

    }
}
