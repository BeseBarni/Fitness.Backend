using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.Domain.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Repositories
{
    public class InstructorRepository : BaseRepository, IInstructorRepository
    {
        public InstructorRepository(AppDbContext context) : base(context)
        {
        }

        public Task Add(Instructor parameters)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Instructor>> GetAll(Instructor parameters)
        {
            throw new NotImplementedException();
        }

        public Task<Instructor> GetOne(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Instructor parameters)
        {
            throw new NotImplementedException();
        }
    }
}
