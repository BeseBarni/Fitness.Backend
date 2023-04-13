using Fitness.Backend.Application.DataContracts.Models;
using Fitness.Backend.Application.DataContracts.Enums;

namespace Fitness.Backend.Application.DataContracts.Models
{
    public class InstructorData
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
        public InstructorStatus? Status { get; set; }
    }
}
