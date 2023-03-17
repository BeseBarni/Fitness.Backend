using Fitness.Backend.Application.DataContracts.Models;

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
