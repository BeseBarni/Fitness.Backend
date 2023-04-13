using Fitness.Backend.Application.DataContracts.Entity;

namespace Fitness.Backend.Application.Contracts.Repositories
{
    public interface ILessonRepository : IBaseRepository<Lesson>
    {
        Task<IEnumerable<User>> GetLessonUsers(string lessonId);
        Task AddClient(string lessonId, string userId);

    }
}
