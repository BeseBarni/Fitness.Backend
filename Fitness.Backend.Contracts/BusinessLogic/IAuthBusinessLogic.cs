﻿using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Models;

namespace Fitness.Backend.Application.Contracts.BusinessLogic
{
    public interface IAuthBusinessLogic
    {
        Task<LoggedInUserData> Login(LoginUser user);
        Task<LoggedInUserData> Register(RegisterUser user);
        Task<UserIdData> CheckEmail(string email);
        Task RegisterWithoutLogin(RegisterUser user, bool instructorPending = false);
        Task<IEnumerable<ApplicationUser>> GetUsers();
    }
}
