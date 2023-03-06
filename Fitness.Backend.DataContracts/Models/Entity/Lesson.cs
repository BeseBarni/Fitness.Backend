using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity.ConnectingEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models.Entity
{
    public class Lesson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public City? City { get; set; }
        public int MaxNumber { get; set; }
        public Sport? Sport { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<LessonUser>? LessonUsers { get; set; }
        public Day Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
