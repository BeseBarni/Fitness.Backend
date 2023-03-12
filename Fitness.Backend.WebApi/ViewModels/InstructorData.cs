using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.Application.DataContracts.Models;

namespace Fitness.Backend.WebApi.ViewModels
{
    public class InstructorData
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
        public InstructorStatus? Status { get; set; }
    }
}
