using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;

namespace USite.Application.Elements.Queries.GetElementsWithPageIdQuery;

[Authorize]
public record GetElementsWithPageIdQuery(Guid PageId) : IRequest<List<ElementDto>>;

public class GetElementsWithPageIdQueryHandler : IRequestHandler<GetElementsWithPageIdQuery, List<ElementDto>>
{
    private readonly ElementsHelper _elementsHelper;

    public GetElementsWithPageIdQueryHandler(ElementsHelper ElementsHelper)
    {
        _elementsHelper= ElementsHelper;
    }

    public async Task<List<ElementDto>> Handle(GetElementsWithPageIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _elementsHelper.MergeElementsForMenu(request.PageId, cancellationToken);

        return ElementDto.Projection(result);
    }
}
