using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;
using USite.Domain.Enums;

namespace USite.Application.Elements.Commands.UpdateElementStyle;

[Authorize]
public record UpdateElementStyleCommand(Guid ElementId, StyleProperty Property, string Value) : IRequest<ElementDto>;

public class UpdateElementCommandHandler : IRequestHandler<UpdateElementStyleCommand, ElementDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateElementCommandHandler> _logger;
    private readonly ElementsHelper _ElementsHelper;

    public UpdateElementCommandHandler(IApplicationDbContext context, ILogger<UpdateElementCommandHandler> logger, ElementsHelper ElementsHelper)
    {
        _context = context;
        _logger = logger;
        _ElementsHelper = ElementsHelper;
    }

    public async Task<ElementDto> Handle(UpdateElementStyleCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Elements.Include(x => x.Styles).FirstOrDefaultAsync(x => x.Id == request.ElementId, cancellationToken) ?? throw new NotFoundException("Element", request.ElementId);

        _logger.LogInformation("Update style for element {ElementId} for property {Property} and value {Value}", request.ElementId, request.Property, request.Value);

        var styleToUpdate = result.Styles.FirstOrDefault(x => x.Property == request.Property);

        if (styleToUpdate == null)
        {
            var newStyle = new Style(request.Property, request.Value);

            result.Styles.Add(newStyle);
            _context.Style.Entry(newStyle).State = EntityState.Added;
        }
        else
        {
            if (request.Value == null)
            {
                result.Styles.Remove(styleToUpdate);
                _context.Style.Entry(styleToUpdate).State = EntityState.Deleted;
            }
            else
                styleToUpdate.Value = request.Value;
        }
        

        await _context.SaveChangesAsync(cancellationToken);

        var element = await _ElementsHelper.GetBaseElementWithId(request.ElementId, cancellationToken);
        return ElementDto.Projection(element);
    }
}
