using USite.Application.Pages.Query.Dto;

namespace USite.Application.Pages.Commands.CreatePage;

[Authorize]
public record CreatePageCommand(string Name, string Description, Guid SiteId) : IRequest<PageDto>;

public class CreatePageCommandHandler : IRequestHandler<CreatePageCommand, PageDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreatePageCommandHandler> _logger;

    public CreatePageCommandHandler(IApplicationDbContext context, ILogger<CreatePageCommandHandler> logger)
    {
        _context = context;
        _logger= logger;
    }

    public async Task<PageDto> Handle(CreatePageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create page for site {id}", request.SiteId);

        var site = await _context.Sites.FirstOrDefaultAsync(s => s.Id == request.SiteId, cancellationToken) ?? throw new NotFoundException(nameof(Page), request.SiteId);

        var entity = new Page(request.Name, request.Description)
        {
            Site = site
        };

        _context.Pages.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return PageDto.Projection(entity);
    }
}
