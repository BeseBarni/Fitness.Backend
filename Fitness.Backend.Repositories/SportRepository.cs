using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Application.DataContracts.Extensions;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.Domain.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Repositories
{
    public class SportRepository : BaseRepository, ISportRepository
    {
        public SportRepository(AppDbContext context) : base(context)
        {
        }

        public async Task Add(Sport parameters)
        {
            if (context.Sports.Where(p => p.Name == parameters.Name).Count() > 0)
                throw new Exception();

            context.Add(parameters);

            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var sport = await context.Sports.DelFilter<Sport>().FirstOrDefaultAsync(p => p.Id == id);
            if (sport == null)
                throw new ResourceNotFoundException();

            sport.Del = 1;
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Sport>> GetAll(Sport? parameters)
        {
            return await context.Sports.DelFilter<Sport>().ToListAsync();

        }

        public async Task<Sport> GetOne(string id)
        {
            var result = await context.Sports.DelFilter<Sport>().FirstOrDefaultAsync(p => p.Id == id);
            if(result == null)
                throw new ResourceNotFoundException();

            return result;
        }

        public async Task Update(Sport parameters)
        {
            var sport = context.Sports.DelFilter<Sport>().FirstOrDefault(p => p.Id == parameters.Id);

            if (sport == null)
                throw new ResourceNotFoundException();

            sport.Name = parameters.Name;
            await context.SaveChangesAsync();
        }
    }
}
