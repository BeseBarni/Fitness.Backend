using AutoMapper;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Application.Extensions;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Distributed;

namespace Fitness.Backend.Application.BusinessLogic
{
    public class InstructorBusinessLogic : IInstructorBusinessLogic
    {
        private readonly IInstructorRepository repo;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;

        public InstructorBusinessLogic(IInstructorRepository repo, IMapper mapper, IDistributedCache cache)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task Add(InstructorData parameters)
        {
            await repo.Add(mapper.Map<Instructor>(parameters));
        }

        public async Task AddSport(string instructorId, string sportId)
        {
            await repo.AddSport(instructorId, sportId);
        }

        public async Task Delete(string id)
        {
            await repo.Delete(id);
        }

        public async Task<IEnumerable<InstructorData>?> GetAll(InstructorData? parameters)
        {
            var key = "get_all_instructor";
            var result = await cache.GetListAsync(key, repo.GetAll, mapper.Map<Instructor>(parameters));

            if (result is null) return Enumerable.Empty<InstructorData>();

            return result.Select(mapper.Map<InstructorData>);
        }

        public async Task<IEnumerable<LessonData>?> GetLessons(string instructorId)
        {
            var key = $"get_{instructorId}_lesson";
            var result = await cache.GetListAsync(key, repo.GetLessons, instructorId);

            if (result is null) return Enumerable.Empty<LessonData>();

            return result.Select(mapper.Map<LessonData>);
        }

        public async Task<InstructorData> GetOne(string id)
        {
            var key = $"get_instructor_{id}";
            var result = await cache.GetAsync(key, repo.GetOne, id);

            return mapper.Map<InstructorData>(result);
        }

        public async Task<IEnumerable<SportData>?> GetSports(string instructorId)
        {
            var key = $"get_{instructorId}_sports";
            var result = await cache.GetListAsync(key, repo.GetSports, instructorId);

            if (result is null) return Enumerable.Empty<SportData>();

            return result.Select(mapper.Map<SportData>);
        }

        public async Task Update(InstructorData parameters)
        {
            await repo.Update(mapper.Map<Instructor>(parameters));
        }
    }
}
