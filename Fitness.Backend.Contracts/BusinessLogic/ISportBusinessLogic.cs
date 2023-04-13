using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.BusinessLogic
{
    public interface ISportBusinessLogic : IBaseBusinessLogic<SportData>
    {
        Task<IEnumerable<InstructorData>?> GetInstructors(string sportId);
    }
}
