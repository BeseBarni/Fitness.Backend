using Fitness.Backend.Application.DataContracts.Models.Entity.ConnectingEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models.Entity
{
    public class Sport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<InstructorSport>? InstructorSports { get; set; }
    }
}
