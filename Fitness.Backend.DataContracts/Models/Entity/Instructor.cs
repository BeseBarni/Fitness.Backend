using Fitness.Backend.Application.DataContracts.Models.Entity.ConnectingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models.Entity
{
    public class Instructor
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public ICollection<InstructorSport>? InstructorSports { get; set; }
        public virtual ICollection<Lesson>? Lessons { get; set; }
        public string? Description { get; set; }
    }
}
