using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models.Entity.ConnectingEntities
{
    public class InstructorSport
    {
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }
    }
}
