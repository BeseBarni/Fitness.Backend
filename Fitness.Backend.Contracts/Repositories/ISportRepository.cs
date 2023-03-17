using Fitness.Backend.Application.DataContracts.Models.Entity;

namespace Fitness.Backend.Application.Contracts.Repositories
{
    public interface ISportRepository : IBaseRepository<Sport>
    {
        Task<IEnumerable<Instructor>?> GetInstructors(string sportId);
    }
}
