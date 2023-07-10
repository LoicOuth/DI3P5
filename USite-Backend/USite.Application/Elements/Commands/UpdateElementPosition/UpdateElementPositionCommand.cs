
using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;
using USite.Domain.Enums;

namespace USite.Application.Elements.Commands.UpdateElementPosition;

[Authorize]
public record UpdateElementPositionCommand(Guid PageId, Guid ElementId, int PositionCounter) : IRequest<List<ElementDto>>;

public class UpdateElementPositionCommandHandler : IRequestHandler<UpdateElementPositionCommand, List<ElementDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateElementPositionCommandHandler> _logger;
    private readonly ElementsHelper _elementsHelper;

    public UpdateElementPositionCommandHandler(IApplicationDbContext context, ILogger<UpdateElementPositionCommandHandler> logger, ElementsHelper ElementsHelper)
    {
        _context = context;
        _logger = logger;
        _elementsHelper = ElementsHelper;
    }

    public async Task<List<ElementDto>> Handle(UpdateElementPositionCommand request, CancellationToken cancellationToken)
    {
        var page = await _context.Pages
                .Include(x => x.Elements)
                .FirstOrDefaultAsync(x => x.Id == request.PageId, cancellationToken)
                 ?? throw new NotFoundException(nameof(Page), request.PageId);

        var element = page.Elements.FirstOrDefault(x => x.Id == request.ElementId) ?? throw new NotFoundException(nameof(BlockElement), request.ElementId);

        _logger.LogInformation("Update position for element {ElementId} with new position : {PositionCounter}", request.ElementId, request.PositionCounter);

        var oldPosition = element.Position;
        var newPosition = element.Position + request.PositionCounter;

        if(element.Type == TypeElement.Block)
        {
            var elementBefore = page.Elements.Where(x => x.Position < oldPosition);
            var elementAfter = page.Elements.Where(x => x.Position > oldPosition);

            if(request.PositionCounter > 0)
            {
                //here the element is mooved down
                foreach (var item in elementAfter.Where(x => x.Position <= newPosition))
                {
                    item.Position -= 1;
                }
            }
            else if(request.PositionCounter < 0)
            {
                foreach (var item in elementBefore.Where(x => x.Position >= newPosition))
                {
                    item.Position += 1;
                }
            }

            element.Position = newPosition;
        }

        await _context.SaveChangesAsync(cancellationToken);
        var elements = await _elementsHelper.MergeElementsForMenu(page.Id, cancellationToken);
        return ElementDto.Projection(elements);
    }
}
