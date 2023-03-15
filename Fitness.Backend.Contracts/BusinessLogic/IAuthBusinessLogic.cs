using Fitness.Backend.Application.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.BusinessLogic
{
    public interface IAuthBusinessLogic
    {
        Task<string> Login(LoginUser user);
        Task<string> Register(RegisterUser user);
        Task<UserIdData> CheckEmail(string email);
        Task RegisterWithoutLogin(RegisterUser user);
        Task<IEnumerable<ApplicationUser>> GetUsers();
    }
}
