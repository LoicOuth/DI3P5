using USite.Domain.Common;

namespace USite.Application.Elements.Helpers;

public class ElementsHelper
{
    private readonly IApplicationDbContext _context;
    public ElementsHelper(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Find the <see cref="BlockElement"/> of a <see cref="Page"/> and all his child <see cref="BaseElement.ElementsChilds"/>
    /// </summary>
    /// <param name="pageId">The id of the Page</param>
    /// <returns>A List of <see cref="BlockElement"/> of a <see cref="Page"/></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<List<BlockElement>> GetElementsWithPageId(Guid pageId, CancellationToken cancellationToken)
    {
        if (!_context.Pages.Any(x => x.Id == pageId)) throw new NotFoundException(nameof(Page), pageId);

        // Load all elements for this page
        var result = await _context.Pages
            .Where(x => x.Id == pageId)
            .SelectMany(x => x.Elements)
            .Where(x => x.Type != Domain.Enums.TypeElement.Link)
            .OrderBy(x => x.Position)
            .Include(x => x.Styles)
            .ToListAsync(cancellationToken);

        foreach (var element in result)
        {
            await LoadChildElements(element, cancellationToken);
        }

        return result;
    }

    public async Task<BlockElement> GetLinkElementWithSiteId(Guid siteId, CancellationToken cancellationToken)
    {
        var menu = _context.Menus.FirstOrDefault(x => x.SiteId == siteId);
        if (menu == null) return null;

        var result = await _context.Elements
            .Where(x => x.MenuId == menu.Id)
            .Include(x => x.Styles).FirstOrDefaultAsync(cancellationToken) as BlockElement;

        await LoadChildElements(result, cancellationToken);

        return result;
    }

    public async Task<List<BlockElement>> MergeElementsForMenu(Guid pageId, CancellationToken cancellationToken)
    {
        var page = await _context.Pages.Where(x => x.Id == pageId).Include(x => x.Site).FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Page), pageId);
        List<BlockElement> elementList = new ();
        var listElement = await GetElementsWithPageId(page.Id, cancellationToken);
        var listMenu = await GetLinkElementWithSiteId(page.Site.Id, cancellationToken);

        elementList.AddRange(listElement);
        if(listMenu != null) elementList.Add(listMenu);

        return elementList;
    }

    /// <summary>
    /// Find the <see cref="BaseElement"/> and his associed <see cref="BaseElement.ElementsChilds"/> and <see cref="Style"/>
    /// </summary>
    /// <param name="id">the id of the Element to find</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<BaseElement> GetBaseElementWithId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Elements
            .Where(x => x.Id == id)
            .Include(x => x.Styles)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(BaseElement), id);

        await LoadChildElements(result, cancellationToken);

        return result;
    }

    /// <summary>
    /// Find the number of ElementChilds for a <see cref="BaseElement"/>
    /// </summary>
    /// <param name="id">The id of the Element</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> ElementChildCount(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Elements
          .Where(x => x.Id == id)
          .SelectMany(x => x.ElementsChilds)
          .CountAsync(cancellationToken);
    }

    public async Task LoadChildElements(BaseElement element, CancellationToken cancellationToken)
    {
        var childElements = await _context.Elements
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
}
