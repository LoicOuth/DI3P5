using USite.Domain.Common;

namespace USite.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Site> Sites { get; }
    DbSet<Page> Pages { get; }
    DbSet<BaseElement> Elements { get; }
    DbSet<Style> Style { get; }
    DbSet<Menu> Menus { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
