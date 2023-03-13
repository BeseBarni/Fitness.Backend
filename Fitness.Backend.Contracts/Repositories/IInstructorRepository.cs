﻿using Fitness.Backend.Application.DataContracts.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.Repositories
{
    public interface IInstructorRepository : IBaseRepository<Instructor>
    {
        Task AddSport(string instructorId, string sportId);
    }
}
