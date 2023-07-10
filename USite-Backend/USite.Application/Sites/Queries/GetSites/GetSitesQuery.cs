namespace USite.Application.Sites.Queries.GetSites;

[Authorize]
public record GetSitesQuery() : IRequest<List<SiteDto>>;

public class GetSitesQueryHanlder : IRequestHandler<GetSitesQuery, List<SiteDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetSitesQueryHanlder(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<SiteDto>> Handle(GetSitesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sites
            .Where(x => x.OwnerId == _currentUserService.UserId!)
            .OrderBy(x => x.Name)
            .Select(x => SiteDto.Projection(x))
            .ToListAsync(cancellationToken);
    }
}