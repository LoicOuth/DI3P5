using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using USite.Application.Common.Interfaces;
using USite.Domain.Common;
using USite.Domain.Entities;
using USite.Infrastructure.Identity;
using USite.Infrastructure.Persistence.Interceptors;

namespace USite.Infrastructure.Persistence;
public class CompleteDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DbSet<Site> Sites => Set<Site>();
    public DbSet<Page> Pages => Set<Page>();
    public DbSet<BaseElement> Elements => Set<BaseElement>();
    public DbSet<Style> Style => Set<Style>();
    public DbSet<Menu> Menus => Set<Menu>();

    public CompleteDbContext(
        DbContextOptions<CompleteDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BaseElement>()
            .HasDiscriminator(x => x.Type)
            .HasValue<BlockElement>(Domain.Enums.TypeElement.Block)
            .HasValue<ImageElement>(Domain.Enums.TypeElement.Image)
            .HasValue<H1Element>(Domain.Enums.TypeElement.H1)
            .HasValue<ButtonElement>(Domain.Enums.TypeElement.Button)
            .HasValue<LinkElement>(Domain.Enums.TypeElement.Link);

        builder.Entity<BaseElement>()
            .HasOne(x => x.Parent)
            .WithMany(x => x.ElementsChilds)
            .HasForeignKey(x => x.ParentId);

        builder.Entity<BaseElement>()
            .HasMany(x => x.Styles)
            .WithOne();

        builder.Entity<Page>()
            .HasOne(x => x.Site)
            .WithMany(x => x.Pages);

        builder.Entity<Menu>()
            .HasOne(x => x.Site);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
