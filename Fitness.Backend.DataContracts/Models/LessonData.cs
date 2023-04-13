using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;

namespace Fitness.Backend.Application.DataContracts.Models
{
    public class LessonData
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int? MaxNumber { get; set; }
        public string? SportId { get; set; }
        public string? InstructorId { get; set; }
        public Day? Day { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
