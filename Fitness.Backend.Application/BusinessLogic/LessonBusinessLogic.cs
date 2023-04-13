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
    public class LessonBusinessLogic : ILessonBusinessLogic
    {
        private readonly ILessonRepository repo;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;

        public LessonBusinessLogic(ILessonRepository repo, IMapper mapper, IDistributedCache cache)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task Add(LessonData parameters)
        {
            await repo.Add(mapper.Map<Lesson>(parameters));
        }

        public async Task AddClient(string lessonId, string userId)
        {
                await repo.AddClient(lessonId,userId);

        }

        public async Task Delete(string id)
        {
            await repo.Delete(id);

        }

        public async Task<IEnumerable<LessonData>?> GetAll(LessonData? parameters)
        {
            var key = "get_all_lesson";
            if (parameters?.InstructorId != null)
                key = key + "_instructorId:" + parameters.InstructorId;
            if (parameters?.SportId != null)
                key = key + "_sportId:" + parameters.SportId;
            if (parameters?.Day != null)
                key = key + "_day:" + parameters.Day;
            var result = await cache.GetListAsync(key, repo.GetAll, mapper.Map<Lesson>(parameters));

            if (result is null) return Enumerable.Empty<LessonData>();

            return result.Select(mapper.Map<LessonData>);
        }

        public async Task<IEnumerable<UserData>?> GetLessonUsers(string lessonId)
        {
            var key = $"get_lesson:{lessonId}_users";
            var result = await cache.GetListAsync(key, repo.GetLessonUsers, lessonId);

            if (result is null) return Enumerable.Empty<UserData>();

            return result.Select(mapper.Map<UserData>);

        }

        public async Task<LessonData> GetOne(string id)
        {
            var key = $"get_lesson:{id}";
            var result = await cache.GetAsync(key, repo.GetOne, id);

            return mapper.Map<LessonData>(result);

        }

        public async Task Update(LessonData parameters)
        {
            await repo.Update(mapper.Map<Lesson>(parameters));
        }
    }
}
