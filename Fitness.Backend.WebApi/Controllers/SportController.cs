using AutoMapper;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Fitness.Backend.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {

        private readonly ISportRepository repo;
        private readonly IMapper mapper;

        public SportController(ISportRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SportData>>> Get()
        {
            var result = await repo.GetAll(null);
                
            return Ok(result.Select(mapper.Map<SportData>));
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
