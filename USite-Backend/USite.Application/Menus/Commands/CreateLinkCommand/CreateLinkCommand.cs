
using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;
using USite.Domain.Common;

namespace USite.Application.Menus.Commands.CreateLinkCommand;

[Authorize]
public record CreateLinkCommand(string Content, Guid PageId, Guid SiteId, Guid CurrentPageId) : IRequest<List<ElementDto>>;

public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, List<ElementDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ElementsHelper _elementsHelper;

    public CreateLinkCommandHandler(IOvhDomainNameHelper ovhHelper, IApplicationDbContext context, ElementsHelper elementsHelper)
    {
        _context = context;
        _elementsHelper = elementsHelper;
    }

    public async Task<List<ElementDto>> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var site = await _context.Sites.FirstOrDefaultAsync(x => x.Id == request.SiteId, cancellationToken) 
            ?? throw new NotFoundException("Site", request.SiteId); ;

        var menu = await _context.Menus.FirstOrDefaultAsync(x => x.Site.Id == request.SiteId, cancellationToken);

        if (menu == null) // Pas de menu on ajoute
        {
            var page = _context.Pages.FirstOrDefault(x => x.Id == request.PageId);
            if (page == null)
                throw new NotFoundException("Page", request.PageId);

            var newMenu = new Menu()
            {
                Site = site,
            };

            await _context.Menus.AddAsync(newMenu, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var block = new BlockElement("Menu", "", 0)
            {
                Menu = newMenu
            };

            block.ElementsChilds = new List<BaseElement>()
            {
                new LinkElement(request.Content, 1)
                {
                    Page = page,
                    ParentId = block.Id
                }
            };

            await _context.Elements.AddAsync(block, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            var elements = await _elementsHelper.MergeElementsForMenu(request.CurrentPageId, cancellationToken);
            return ElementDto.Projection(elements);
        }
        else
        {
            var element = _context.Elements.FirstOrDefault(x => x.MenuId == menu.Id);
            var position = element.ElementsChilds.Count() + 1;
            if (element == null)
                throw new NotFoundException("Element with MenuId", menu.Id);

            var page = _context.Pages.FirstOrDefault(x => x.Id == request.PageId);

            if (page == null)
                throw new NotFoundException("Page", request.PageId);

            var link = new LinkElement(request.Content, position)
            {
                ParentId = element.Id,
                Page = page
            };

            await _context.Elements.AddAsync(link, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var elements = await _elementsHelper.MergeElementsForMenu(request.CurrentPageId, cancellationToken);
            return ElementDto.Projection(elements);
        }
    }
}