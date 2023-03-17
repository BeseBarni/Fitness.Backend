using Fitness.Backend.Application.DataContracts.Enums;

namespace Fitness.Backend.Application.DataContracts.Models.ViewModels
{
    public class RegisterUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public bool IsInstructor { get; set; }
    }
}
