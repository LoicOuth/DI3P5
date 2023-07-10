using USite.Application.Pages.Commands.CreatePage;
using USite.Application.Pages.Query.Dto;

namespace USite.Application.Pages.Commands.UpdatePage;

[Authorize]
public record UpdatePageCommand(string Name, string Description, bool IsFirst, Guid PageId) : IRequest<PageDto>;

public class UpdatePageCommandHandler : IRequestHandler<UpdatePageCommand, PageDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreatePageCommandHandler> _logger;

    public UpdatePageCommandHandler(IApplicationDbContext context, ILogger<CreatePageCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PageDto> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("update page with id : {id}", request.PageId);

        var ps = await _context.Pages.Select(x => new {Page = x, x.Site}).FirstOrDefaultAsync(x => x.Page.Id == request.PageId, cancellationToken) ?? throw new NotFoundException(nameof(Page), request.PageId);
        
        if (request.IsFirst)
        {
            var siteId = ps.Site.Id;
            var otherPages = await _context.Pages.Where(x => x.Site.Id == siteId && x.Id != ps.Page.Id).ToListAsync(cancellationToken);
            foreach (var otherPage in otherPages)
            {
                otherPage.IsFirst = false;
            }
        }
        else
        {
            var existingIsFirstPage = await _context.Pages.FirstOrDefaultAsync(x => x.Site.Id == ps.Site.Id && x.IsFirst, cancellationToken);
            if (existingIsFirstPage == ps.Page)
            {
                var randomPage = await _context.Pages.Where(x => x.Site.Id == ps.Site.Id && x.Id != ps.Page.Id).ToListAsync(cancellationToken);
                var randomIndex = new Random().Next(randomPage.Count);
                randomPage[randomIndex].IsFirst = true;
            }
        }

        ps.Page.Name = request.Name;
        ps.Page.Description = request.Description;
        ps.Page.IsFirst = request.IsFirst;

        await _context.SaveChangesAsync(cancellationToken);

        return PageDto.Projection(ps.Page);
    }
}
