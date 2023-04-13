using Fitness.Backend.Application.DataContracts.Enums;
using Fitness.Backend.Application.DataContracts.Models;

namespace Fitness.Backend.Application.DataContracts.Models
{
    public class UserData
    {
        public string? Id { get; set; }
        public string? ImageId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Gender? Gender { get; set; }

    }
}
