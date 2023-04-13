using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.BusinessLogic
{
    public interface IUserBusinessLogic : IBaseBusinessLogic<UserData>
    {
        Task AddLesson(string lessonId, string userId);
        Task<IEnumerable<LessonData>> GetLessons(string userId);
        Task<Image> GetImage(string userId);
        Task AddImage(string userId, IFormFile image);
    }
}
