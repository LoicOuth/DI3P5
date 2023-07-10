namespace USite.Application.Sites.Queries.GetSiteFromIdQuery;

[Authorize]
public record GetSiteFromIdQuery(Guid SiteId) : IRequest<SiteDto>;

public class GetSiteFromIdQueryHandler : IRequestHandler<GetSiteFromIdQuery, SiteDto>
{
    private readonly IApplicationDbContext _context;

    public GetSiteFromIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SiteDto> Handle(GetSiteFromIdQuery request, CancellationToken cancellationToken)
    {
        var site = await _context.Sites.FirstOrDefaultAsync(x => x.Id == request.SiteId, cancellationToken) ?? throw new NotFoundException(nameof(Site), request.SiteId);
        return SiteDto.Projection(site);
    }
}
