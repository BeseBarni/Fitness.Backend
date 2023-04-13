using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models
{
    public class LoggedInUserData : UserData
    {
        public string JwtToken { get; set; }
    }
}
