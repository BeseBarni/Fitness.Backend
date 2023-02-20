using Duende.IdentityServer.EntityFramework.Options;
using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Fitness.Backend.WebApi.Data
{
    public class AuthDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}
