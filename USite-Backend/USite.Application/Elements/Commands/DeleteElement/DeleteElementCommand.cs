using USite.Domain.Common;
using USite.Domain.Enums;

namespace USite.Application.Elements.Commands.DeleteElement;

[Authorize]
public record DeleteElementCommand(Guid ElementId) : IRequest<Guid>;

public class DeleteElementCommandHandler : IRequestHandler<DeleteElementCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public readonly ILogger<DeleteElementCommandHandler> _logger;
    public readonly IAzureFileStorageHelper _azurStorage;

    public DeleteElementCommandHandler(IApplicationDbContext context, ILogger<DeleteElementCommandHandler> logger, IAzureFileStorageHelper azurStorage)
    {
        _context = context;
        _logger = logger;
        _azurStorage = azurStorage;
    }

    public async Task<Guid> Handle(DeleteElementCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete element {ElementId}", request.ElementId);

        var result = await _context.Elements.FirstOrDefaultAsync(x => x.Id == request.ElementId, cancellationToken) ?? throw new NotFoundException("Element", request.ElementId);
        
        await DeleteElement(result, cancellationToken);
        return result.Id;
    }

    private async Task DeleteElement(BaseElement element, CancellationToken cancellationToken)
    {
        var relatedStyles = await _context.Style.Where(x => x.BaseElementId == element.Id).ToListAsync();
        _context.Style.RemoveRange(relatedStyles);

        await _context.SaveChangesAsync(cancellationToken);

        var childElements = await _context.Elements.Where(x => x.ParentId == element.Id).ToListAsync();

        foreach (var childElement in childElements)
        {
            await DeleteElement(childElement, cancellationToken);
        }

        _context.Elements.Remove(element);

        await _context.SaveChangesAsync(cancellationToken);

        if (element.Type == TypeElement.Image)
        {
            await _azurStorage.DeleteFileIfExist(element.Id.ToString());
        }
    }
}
