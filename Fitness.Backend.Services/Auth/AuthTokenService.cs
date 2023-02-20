using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Fitness.Backend.Services.Auth
{
    public class AuthTokenService
    {
        private readonly IConfiguration _configuration;
        public AuthTokenService(IConfiguration config)
        {
            _configuration = config;
        }
    }
}
