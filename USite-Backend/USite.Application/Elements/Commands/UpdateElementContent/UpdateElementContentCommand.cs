using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;
using USite.Domain.Common;

namespace USite.Application.Elements.Commands.UpdateElementContent;

[Authorize]
public record UpdateElementContentCommand(Guid ElementId, string Content) : IRequest<ElementDto>;


public class UpdateElementContentCommandHandler : IRequestHandler<UpdateElementContentCommand, ElementDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateElementContentCommandHandler> _logger;
    private readonly ElementsHelper _ElementsHelper;

    public UpdateElementContentCommandHandler(IApplicationDbContext context, ILogger<UpdateElementContentCommandHandler> logger, ElementsHelper ElementsHelper)
    {
        _context = context;
        _logger = logger;
        _ElementsHelper = ElementsHelper;
    }

    public async Task<ElementDto> Handle(UpdateElementContentCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Elements.FirstOrDefaultAsync(x => x.Id == request.ElementId, cancellationToken) ?? throw new NotFoundException("Element", request.ElementId);

        if (!result.GetType().IsSubclassOf(typeof(BaseElementWithContent))) 
            throw new NotFoundException("Element has no content", request.ElementId);

        _logger.LogInformation("Update content for element {ElementId} with new content : {Content}", request.ElementId, request.Content);

        var resultWithContent = result as BaseElementWithContent;
        resultWithContent.Content = request.Content;

        await _context.SaveChangesAsync(cancellationToken);
        var element = await _ElementsHelper.GetBaseElementWithId(request.ElementId, cancellationToken);
        return ElementDto.Projection(element);
    }
}