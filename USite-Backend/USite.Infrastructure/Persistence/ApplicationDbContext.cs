using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using Moq;
using System.Reflection;
using USite.Application.Common.Interfaces;
using USite.Domain.Common;
using USite.Domain.Entities;
using USite.Infrastructure.Persistence.Interceptors;

namespace USite.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DbSet<Site> Sites => Set<Site>();
    public DbSet<Page> Pages => Set<Page>();
    public DbSet<BaseElement> Elements => Set<BaseElement>();
    public DbSet<Style> Style => Set<Style>();
    public DbSet<Menu> Menus => Set<Menu>();

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}

public class CompleteDbContextFactory : IDesignTimeDbContextFactory<CompleteDbContext>
{
    public CompleteDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CompleteDbContext>();
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=USite;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        return new CompleteDbContext(optionsBuilder.Options,
             Options.Create(new OperationalStoreOptions()),
            new AuditableEntitySaveChangesInterceptor(Mock.Of<ICurrentUserService>()));
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=USite;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        return new ApplicationDbContext(optionsBuilder.Options,
            new AuditableEntitySaveChangesInterceptor(Mock.Of<ICurrentUserService>()));
    }
}

public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=USite;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        return new IdentityDbContext(optionsBuilder.Options,
            Options.Create(new OperationalStoreOptions()));
    }
}
