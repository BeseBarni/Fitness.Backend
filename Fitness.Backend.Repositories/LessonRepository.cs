using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Repositories
{
    public class LessonRepository : ILessonRepository
    {

        //todo EF core db, db context implementation

        //Mock Data
        public async Task<IEnumerable<Lesson>> GetLessonListByInstructorAsync(string instructorId)
        {
            var result = new List<Lesson>()
            {
                new Lesson() {Name = "Pilates", SportType = "Pilates", Date = DateTime.Today},
                new Lesson() {Name = "Yoga", SportType = "Yoga", Date = DateTime.Today.AddDays(1)},
                new Lesson() {Name = "Karate", SportType = "Karate", Date = DateTime.Today.AddDays(2)},
                new Lesson() {Name = "Spinning", SportType = "Spinning", Date = DateTime.Today.AddDays(3)},
            };
            return await Task.Factory.StartNew(() =>
            {
                //Simulating work
                
                Thread.Sleep(200);
                return result;
            });

        }
    }
}
