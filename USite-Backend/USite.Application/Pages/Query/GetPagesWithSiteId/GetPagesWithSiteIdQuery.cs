using USite.Application.Pages.Query.Dto;

namespace USite.Application.Pages.Query.GetAllPageWithSiteId;

[Authorize]
public record GetPagesWithSiteIdQuery(Guid SiteId) : IRequest<List<PageDto>>;

public class GetPagesWithSiteIdQueryHandler : IRequestHandler<GetPagesWithSiteIdQuery, List<PageDto>>
{
    private readonly IApplicationDbContext _context;

    public GetPagesWithSiteIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PageDto>> Handle(GetPagesWithSiteIdQuery request, CancellationToken cancellationToken)
    {
        if (!_context.Pages.Any(x => x.Site.Id == request.SiteId)) throw new NotFoundException(nameof(Page), request.SiteId);

        var result = await _context.Pages
            .Where(x => x.Site.Id == request.SiteId)
            .OrderByDescending(x => x.IsFirst)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return PageDto.Projection(result);
    }
}
