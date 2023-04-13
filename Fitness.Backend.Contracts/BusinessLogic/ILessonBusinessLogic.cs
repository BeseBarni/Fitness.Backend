using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.BusinessLogic
{
    public interface ILessonBusinessLogic : IBaseBusinessLogic<LessonData>
    {
        Task<IEnumerable<UserData>?> GetLessonUsers(string lessonId);
        Task AddClient(string lessonId, string userId);
    }
}
