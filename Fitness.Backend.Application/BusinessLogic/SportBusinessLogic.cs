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
    public class SportBusinessLogic : ISportBusinessLogic
    {
        private readonly ISportRepository repo;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;

        public SportBusinessLogic(ISportRepository repo, IMapper mapper, IDistributedCache cache)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task Add(SportData parameters)
        {
            await repo.Add(mapper.Map<Sport>(parameters));
            await cache.RemoveAsync("get_all_sports");
        }

        public async Task Delete(string id)
        {
            await repo.Delete(id);
            await cache.RemoveAsync("get_all_sports");


        }

        public async Task<IEnumerable<SportData>?> GetAll(SportData? parameters)
        {
            var key = "get_all_sports";
            var result = await cache.GetListAsync<Sport>(key, repo.GetAll,null);

            if(result is null) return Enumerable.Empty<SportData>();

            return result.Select(mapper.Map<SportData>);
        }

        public async Task<IEnumerable<InstructorData>?> GetInstructors(string sportId)
        {
            var key = $"get_sport:{sportId}_instructors";
            var result = await cache.GetListAsync(key, repo.GetInstructors, sportId);

            if (result is null) return Enumerable.Empty<InstructorData>();

            return result.Select(mapper.Map<InstructorData>);
        }

        public async Task<SportData> GetOne(string id)
        {
            var key = $"get_sport:{id}";
            var result = await cache.GetAsync(key, repo.GetOne, id);

            return mapper.Map<SportData>(result);
        }

        public async Task Update(SportData parameters)
        {
            await repo.Update(mapper.Map<Sport>(parameters));
            await cache.RemoveAsync("get_all_sports");

        }

        public async Task<Image> GetImage(string sportId)
        {
            var key = $"sport:{sportId}_image";
            var result = await cache.GetAsync(key, repo.GetImage, sportId);

            return result;

        }

        public async Task AddImage(string sportId, IFormFile image)
        {
            await repo.AddImage(sportId, image);
        }
    }
}
