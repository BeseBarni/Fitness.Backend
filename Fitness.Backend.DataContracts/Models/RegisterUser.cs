using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models
{
    public class RegisterUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsInstructor { get; set; }
    }
}
