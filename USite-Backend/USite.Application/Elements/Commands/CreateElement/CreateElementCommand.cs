using Microsoft.Extensions.Configuration;
using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;
using USite.Domain.Enums;

namespace USite.Application.Elements.Commands.CreateElement;

[Authorize]
public record CreateElementCommand(TypeElement TypeElement, string Name, string Description, Guid IdParent) : IRequest<ElementDto>;

public class CreateElementCommandHandler : IRequestHandler<CreateElementCommand, ElementDto>
{
    private readonly ElementsHelper _elementsHelper;
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateElementCommandHandler> _logger;
    public readonly IConfiguration _config;

    public CreateElementCommandHandler(IApplicationDbContext context, ILogger<CreateElementCommandHandler> logger, ElementsHelper elementsHelper, IConfiguration config)
    {
        _elementsHelper = elementsHelper;
        _context = context;
        _logger = logger;
        _config = config;
    }

    public async Task<ElementDto> Handle(CreateElementCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create {type} for parent {id}", nameof(request.TypeElement), request.IdParent);

        return request.TypeElement switch
        {
            TypeElement.Block => await HandleBlock(request.Name, request.Description, request.IdParent, cancellationToken),
            TypeElement.Image => await HandleImage(request.IdParent, cancellationToken),
            TypeElement.H1 => await HandleH1(request.IdParent, cancellationToken),
            TypeElement.Button => await HandleButton(request.IdParent, cancellationToken),
            _ => throw new NotImplementedException(),
        };
    }

    private async Task<ElementDto> HandleBlock(string name, string description, Guid parentId, CancellationToken cancellationToken)
    {
        var page = await _context.Pages.SingleOrDefaultAsync(x => x.Id == parentId, cancellationToken);
        dynamic parent;

        if(page == null)
            parent = await _context.Elements
                .Select(x => new { Element = x, Elements = x.ElementsChilds })
                .FirstAsync(x => x.Element.Id == parentId, cancellationToken);
        else
            parent = await _context.Pages
                .Select(x => new { Element = x, x.Elements })
                .FirstAsync(x => x.Element.Id == parentId, cancellationToken);

        var entity = new BlockElement(name, description, parent.Elements.Count + 1)
        {
            Styles = new List<Style>()
            {
                new Style(StyleProperty.Flex, "flex")
            }
        };

        if(page == null)
        {
            entity.Parent = parent.Element;
            entity.Styles.Add(new Style(StyleProperty.Width, "w-[100%]"));
        }
        else
            entity.Page = parent.Element;

        _context.Elements.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return ElementDto.Projection(entity);
    }

    private async Task<ElementDto> HandleH1(Guid idParent, CancellationToken cancellationToken)
    {
        var count = await _elementsHelper.ElementChildCount(idParent, cancellationToken);
        var entity = new H1Element("text", count + 1)
        {
            ParentId = idParent
        };

        _context.Elements.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return ElementDto.Projection(entity);
    }

    private async Task<ElementDto> HandleButton(Guid idParent, CancellationToken cancellationToken)
    {
        var count = await _elementsHelper.ElementChildCount(idParent, cancellationToken);
        var entity = new ButtonElement("button", count + 1)
        {
            ParentId = idParent
        };

        _context.Elements.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return ElementDto.Projection(entity);
    }

    private async Task<ElementDto> HandleImage(Guid idParent, CancellationToken cancellationToken)
    {
        string defaultUrl = _config.GetSection("AzureStorage")["DefaultImageUrl"] ?? throw new InvalidOperationException("AzureStorage DefaultImageUrl is not configured"); ;
        var count = await _elementsHelper.ElementChildCount(idParent, cancellationToken);
        var entity = new ImageElement(count + 1, defaultUrl, "image_element")
        {
            ParentId = idParent,
            Styles = new List<Style>()
            {
                new Style(StyleProperty.Width, "w-[auto]"),
            }
        };

        _context.Elements.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return ElementDto.Projection(entity);
    }
}
