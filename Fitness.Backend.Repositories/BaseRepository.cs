using Fitness.Backend.Application.DataContracts.Models.Entity.Interfaces;
using Fitness.Backend.Domain.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Repositories
{
    /// <summary>
    /// Base repository to expose Db context to all inheriting classes
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

    }
}
