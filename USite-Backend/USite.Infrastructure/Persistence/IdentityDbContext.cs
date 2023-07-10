using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using USite.Infrastructure.Identity;

namespace USite.Infrastructure.Persistence;

public class IdentityDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public IdentityDbContext(
        DbContextOptions<IdentityDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }
}
