using Microsoft.AspNetCore.Http;
using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;

namespace USite.Application.Elements.Commands.UpdateElementUrl;

[Authorize]
public record UpdateElementUrlCommand(Guid ElementId, IFormFile File) : IRequest<ElementDto>;

public class UpdateElementUrlCommandHandler : IRequestHandler<UpdateElementUrlCommand, ElementDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateElementUrlCommand> _logger;
    private readonly ElementsHelper _elementsHelper;
    private readonly IAzureFileStorageHelper _fileStorage;
    public UpdateElementUrlCommandHandler(IApplicationDbContext context, ILogger<UpdateElementUrlCommand> logger, ElementsHelper elementsHelper, IAzureFileStorageHelper fileStorage)
    {
        _context = context;
        _logger = logger;
        _elementsHelper = elementsHelper;
        _fileStorage = fileStorage;
    }

    public async Task<ElementDto> Handle(UpdateElementUrlCommand request, CancellationToken cancellationToken)
    {
        var result = await _elementsHelper.GetBaseElementWithId(request.ElementId, cancellationToken)
            ?? throw new NotFoundException("Element", request.ElementId);

        var resultImage = result as ImageElement;

        var url = await _fileStorage.UploadFile(request.File, resultImage.Url);
        
        _logger.LogInformation("Update url for element {ElementId} with new url : {Url}", request.ElementId, request.ElementId);

        resultImage.Url = url;
        await _context.SaveChangesAsync(cancellationToken);

        return ElementDto.Projection(resultImage);
    }

}
