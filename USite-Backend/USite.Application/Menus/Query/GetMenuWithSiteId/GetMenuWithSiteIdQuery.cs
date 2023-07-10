using USite.Application.Elements.Helpers;
using USite.Application.Menus.Query.Dto;

namespace USite.Application.Menus.Query.GetMenuWithSiteId;
public record GetMenuWithSiteIdQuery(Guid SiteId) : IRequest<MenuDto>;

public class GetMenuWithSiteIdQueryHandler : IRequestHandler<GetMenuWithSiteIdQuery, MenuDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ElementsHelper _getElementsHelper;

    public GetMenuWithSiteIdQueryHandler(IApplicationDbContext context, ElementsHelper getElementsHelper)
    {
        _context = context;
        _getElementsHelper = getElementsHelper;
    }

    public async Task<MenuDto> Handle(GetMenuWithSiteIdQuery request, CancellationToken cancellationToken)
    {
        if (!_context.Menus.Any(x => x.Site.Id == request.SiteId)) throw new NotFoundException($"Entity Menu with Site.Id {request.SiteId} was not found");

        var menu = await _context.Menus.FirstOrDefaultAsync(x => x.SiteId == request.SiteId, cancellationToken);

        return MenuDto.Projection(menu);
    }
}