using AutoMapper;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Application.Extensions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace Fitness.Backend.Application.BusinessLogic
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly IUserRepository repo;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;

        public UserBusinessLogic(IUserRepository repo, IMapper mapper, IDistributedCache cache)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task Add(UserData parameters)
        {
            await repo.Add(mapper.Map<User>(parameters));
        }

        public async Task AddImage(string userId, IFormFile image)
        {
            await repo.AddImage(userId, image);
            var key = $"user:{userId}_image";
            await cache.RemoveAsync(key);
        }

        public async Task AddLesson(string lessonId, string userId)
        {
            await repo.AddLesson(lessonId, userId);
        }

        public async Task Delete(string id)
        {
            await repo.Delete(id);
        }

        public async Task<IEnumerable<UserData>?> GetAll(UserData? parameters)
        {
            var key = "get_all_users";
            var result = await cache.GetListAsync(key, repo.GetAll, mapper.Map<User>(parameters));

            if (result is null) return Enumerable.Empty<UserData>();

            return result.Select(mapper.Map<UserData>);
        }

        public async Task<Image> GetImage(string userId)
        {
            var key = $"user:{userId}_image";
            var result = await cache.GetAsync(key, repo.GetImage, userId);

            return result;

        }

        public async Task<IEnumerable<LessonData>> GetLessons(string userId)
        {
            var key = $"get_user:{userId}_lessons";
            var result = await cache.GetListAsync(key, repo.GetLessons, userId);

            if (result is null) return Enumerable.Empty<LessonData>();

            return result.Select(mapper.Map<LessonData>);
        }

        public async Task<UserData> GetOne(string id)
        {
            var key = $"get_user:{id}";
            var result = await cache.GetAsync(key, repo.GetOne, id);

            return mapper.Map<UserData>(result);
        }

        public async Task Update(UserData parameters)
        {
            await repo.Update(mapper.Map<User>(parameters));
        }
    }
}
