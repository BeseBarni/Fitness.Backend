using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fitness.Backend.Application.Contracts.Services
{
    public interface IAuthTokenService
    {

        public JwtSecurityToken GenerateToken(List<Claim> authClaims);
    }
}
