using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;
using USite.Domain.Common;

namespace USite.Application.Menus.Commands.UpdateLinkPositionCommand;

[Authorize]
public record UpdateLinkPositionCommand(Guid PageId, Guid ElementId, int PositionCounter) : IRequest<List<ElementDto>>;

public class UpdateLinkPositionCommandHandler : IRequestHandler<UpdateLinkPositionCommand, List<ElementDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateLinkPositionCommandHandler> _logger;
    private readonly ElementsHelper _getElementsHelper;

    public UpdateLinkPositionCommandHandler(IApplicationDbContext context, ILogger<UpdateLinkPositionCommandHandler> logger, ElementsHelper getElementsHelper)
    {
        _context = context;
        _logger = logger;
        _getElementsHelper = getElementsHelper;
    }

    public async Task<List<ElementDto>> Handle(UpdateLinkPositionCommand request, CancellationToken cancellationToken)
    {
        var page = await _context.Pages.Where(x => x.Id == request.PageId).Include(x => x.Site).FirstOrDefaultAsync();
        if (page == null) 
            throw new NotFoundException("Site", request.PageId);

        var menu = await _context.Menus.FirstOrDefaultAsync(x => x.SiteId == page.Site.Id);
        if (menu == null) 
            throw new NotFoundException("Site", page.Site.Id);

        var element = await _context.Elements.FirstOrDefaultAsync(x => x.Id == request.ElementId);

        if (element == null)
            throw new NotFoundException("Element", request.ElementId);

        _logger.LogInformation("Update position for link {ElementId} with new position : {PositionCounter}", request.ElementId, request.PositionCounter);

        var oldPosition = element.Position;
        var newPosition = element.Position + request.PositionCounter;

        var menuElement = await _context.Elements.Where(x => x.Id == element.ParentId)
            .Include(x => x.ElementsChilds)
            .FirstOrDefaultAsync() as BlockElement;

        List<BaseElement> linkBefore = new();
        List<BaseElement> linkAfter = new();

        foreach (var item in menuElement.ElementsChilds)
        {
            if (item.Position < oldPosition) linkBefore.Add(item);
            if (item.Position > oldPosition) linkAfter.Add(item);
        }

        if (request.PositionCounter > 0)
        {
            foreach (var item in linkAfter.Where(x => x.Position <= newPosition))
            {
                item.Position -= 1;
            }
        }
        else if (request.PositionCounter < 0)
        {
            foreach (var item in linkBefore.Where(x => x.Position >= newPosition))
            {
                item.Position += 1;
            }
        }

        element.Position = newPosition;

        await _context.SaveChangesAsync(cancellationToken);
        var elements = await _getElementsHelper.MergeElementsForMenu(page.Id, cancellationToken);

        return ElementDto.Projection(elements);
    }
}