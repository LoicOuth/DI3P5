using USite.Application.Elements.Queries.Dto;
using USite.Application.Template.Dtos;
using USite.Domain.Common;

namespace USite.Application.Template.Commands.AddTemplateBlockInSiteCommand;

[Authorize]
public record AddTemplateBlockToPageCommand(Guid TemplateId, Guid PageId) : IRequest<TemplateDto>;

public class AddTemplateBlockToPageCommandHandler : IRequestHandler<AddTemplateBlockToPageCommand, TemplateDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AddTemplateBlockToPageCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<TemplateDto> Handle(AddTemplateBlockToPageCommand request, CancellationToken cancellationToken)
    {
        var page = await _context.Pages.Select(x => new { Page = x, x.Elements, x.Site }).FirstOrDefaultAsync(x => x.Page.Id == request.PageId, cancellationToken) 
            ?? throw new InvalidOperationException($"No Page found with Id : {request.PageId}");

        if (page.Site.OwnerId != _currentUserService.UserId)
            throw new ForbiddenAccessException(nameof(Site), page.Site.Id.ToString());

        var template = await _context.Elements.AsNoTracking().Include(x => x.Styles).FirstOrDefaultAsync(x => x.Id == request.TemplateId, cancellationToken)
               ?? throw new InvalidOperationException($"No template found with Id : {request.TemplateId}");

        await LoadChildElements(template, cancellationToken);

        template.PageId = request.PageId;
        template.Position = page.Elements.Count + 1;
        (template as BlockElement).IsTemplate = false;
        template = DeleteId(template) as BlockElement;

        _context.Elements.Add(template);

        await _context.SaveChangesAsync(cancellationToken);

        return new(page.Site.Id, ElementDto.Projection(template));
    }

    private async Task LoadChildElements(BaseElement element, CancellationToken cancellationToken)
    {
        var childElements = await _context.Elements
            .AsNoTracking()
            .Where(x => x.ParentId == element.Id)
            .Include(x => x.Styles)
            .OrderBy(x => x.Position)
            .ToListAsync(cancellationToken);

        element.ElementsChilds = childElements;

        foreach (var childElement in childElements)
        {
            await LoadChildElements(childElement, cancellationToken);
        }
    }

    private BaseElement DeleteId(BaseElement element)
    {
        element.Id = Guid.NewGuid();

        var newStyles = new List<Style>();
        foreach(var style in element.Styles)
        {
            style.Id = Guid.NewGuid();
            style.BaseElementId = element.Id;
            newStyles.Add(style);
        }

        element.Styles = newStyles;

        if (element.ElementsChilds.Any())
        {
            var newElementChilds = new List<BaseElement>();
            foreach (var elementChild in element.ElementsChilds)
            {
                var newElement = DeleteId(elementChild);
                newElement.ParentId = element.Id;
                newElementChilds.Add(newElement);
            }

            element.ElementsChilds = newElementChilds;
        }

        return element;
    }
}