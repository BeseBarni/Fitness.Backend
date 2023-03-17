using AutoMapper;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Models.Entity.DatabaseEntities;
using Fitness.Backend.Application.DataContracts.Models.ViewModels;
using Fitness.Backend.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Fitness.Backend.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {

        private readonly ISportRepository repo;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;

        public SportController(ISportRepository repo, IMapper mapper, IDistributedCache cache)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.cache = cache;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SportData>>> Get()
        {
            var key = "get_all_sports";
            var result = await cache.GetRecordAsync<IEnumerable<SportData>>(key);
            if(result == null)
            {
                var temp = await repo.GetAll(null);
                Console.WriteLine("Getting sports from db");
                result = temp.Select(mapper.Map<SportData>);
                await cache.SetRecordAsync(key,result);
            }
                
            return Ok(result);
        }
        [HttpGet("{sportId}/Instructors")]
        public async Task<ActionResult<IEnumerable<SportData>>> GetInstructors(string sportId)
        {
            var result = await repo.GetInstructors(sportId);

            return Ok(mapper.Map<InstructorData>(result));
        }

        [HttpDelete("{sportId}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string sportId)
        {
            await repo.Delete(sportId);

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Post([FromBody] SportData sport)
        {
            await repo.Add(mapper.Map<Sport>(sport));
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] SportData sport)
        {
            await repo.Update(mapper.Map<Sport>(sport));


            return NoContent();
        }
    }
}
