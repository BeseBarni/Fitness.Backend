using Fitness.Backend.Application.DataContracts.Models.Entity.ConnectingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models.Entity
{
    public class User
    {
        public string Id { get; set; }

        public ICollection<LessonUser>? LessonUsers { get; set; }
        public string? Name { get; set; }
        public City? City { get; set; }
    }
}
