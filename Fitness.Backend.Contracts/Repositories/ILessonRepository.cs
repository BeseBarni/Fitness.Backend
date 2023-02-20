using Fitness.Backend.Application.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.Repositories
{
    public interface ILessonRepository
    {
        public Task<IEnumerable<Lesson>> GetLessonListByInstructorAsync(string instructorId);


    }
}
