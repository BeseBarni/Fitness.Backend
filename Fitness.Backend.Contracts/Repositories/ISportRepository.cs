using Fitness.Backend.Application.DataContracts.Entity;
using Microsoft.AspNetCore.Http;

namespace Fitness.Backend.Application.Contracts.Repositories
{
    public interface ISportRepository : IBaseRepository<Sport>
    {
        Task<IEnumerable<Instructor>> GetInstructors(string sportId);
        Task<Image> GetImage(string sportId);
        Task AddImage(string sportId, IFormFile image);
    }
}
