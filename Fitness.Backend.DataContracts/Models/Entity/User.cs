
using Fitness.Backend.Application.DataContracts.Models.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models.Entity
{
    public class User : IDeleteable, IDateTrackeable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int? ImageId { get; set; }
        public virtual Image? ProfilePic { get; set; }
        public virtual ICollection<Lesson>? Lessons { get; set; }
        public string? Name { get; set; }
        public int Del { get; set; } = 0;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
