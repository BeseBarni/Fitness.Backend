using Fitness.Backend.Application.DataContracts.Entity;
using Microsoft.AspNetCore.Http;

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
