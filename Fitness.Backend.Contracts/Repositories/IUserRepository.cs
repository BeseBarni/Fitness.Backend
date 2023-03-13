using Fitness.Backend.Application.DataContracts.Models.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task AddLesson(string lessonId, string userId);
        Task<IEnumerable<Lesson>> GetLessons(string userId);
        Task<Image> GetImage(string userId);
        Task AddImage(string userId, IFormFile image);
    }
}
