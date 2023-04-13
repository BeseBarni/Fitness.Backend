using Duende.IdentityServer.EntityFramework.Options;
using Fitness.Backend.Application.DataContracts.Entity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Fitness.Backend.Domain.DbContexts
{
    /// <summary>
    /// Db context for the identity auth server
    /// </summary>
    public class AuthDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

        }
    }
}
