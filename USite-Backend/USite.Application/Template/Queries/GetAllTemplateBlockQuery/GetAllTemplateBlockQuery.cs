using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;

namespace USite.Application.Template.Queries.GetAllTemplateQuery;

[Authorize]
public record GetAllTemplateBlockQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<ElementDto>>;

public class GetAllTemplateBlockQueryHandler : IRequestHandler<GetAllTemplateBlockQuery, PaginatedList<ElementDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ElementsHelper _elementsHelper;

    public GetAllTemplateBlockQueryHandler(IApplicationDbContext context, ElementsHelper elementsHelper)
    {
        _context = context;
        _elementsHelper = elementsHelper;
    }

    public async Task<PaginatedList<ElementDto>> Handle(GetAllTemplateBlockQuery request, CancellationToken cancellationToken)
    {
        var templatesQuery = _context.Elements
            .Where(x => ((BlockElement)x).IsTemplate)
            .Include(x => x.Styles);

        var count = await templatesQuery.CountAsync();

        var templates = await templatesQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        foreach (var template in templates)
        {
            await _elementsHelper.LoadChildElements(template, cancellationToken);
        }

        return new(ElementDto.Projection(templates), count, request.PageNumber, request.PageSize);
    }
}
