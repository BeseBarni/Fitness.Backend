using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Entity
{
    public class Lesson : IDeleteable, IDateTrackeable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int? MaxNumber { get; set; }
        public string? SportId { get; set; }
        public Sport? Sport { get; set; }
        public string? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public Day? Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Del { get; set; } = 0;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
