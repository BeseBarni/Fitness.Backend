using Fitness.Backend.Application.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.Services
{
    public interface IAuthTokenService
    {

        public JwtSecurityToken GenerateToken(List<Claim> authClaims);
    }
}
