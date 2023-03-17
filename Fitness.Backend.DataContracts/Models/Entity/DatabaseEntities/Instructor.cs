using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models.Entity.DatabaseEntities
{
    public class Instructor : IDeleteable, IDateTrackeable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Sport>? Sports { get; set; }
        public virtual ICollection<Lesson>? Lessons { get; set; }
        public string? Description { get; set; }
        public InstructorStatus? Status { get; set; }
        public int Del { get; set; } = 0;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
