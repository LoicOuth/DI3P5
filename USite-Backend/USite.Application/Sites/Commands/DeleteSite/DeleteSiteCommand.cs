namespace USite.Application.Sites.Commands.DeleteSite;

[Authorize]
public record DeleteSiteCommand(Guid Id) : IRequest;

public class DeleteSiteCommandHandler : IRequestHandler<DeleteSiteCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteSiteCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sites.Select(x => new
        {
            Site = x,
            x.Pages,
            Elements = x.Pages.SelectMany(x => x.Elements)
        }).FirstOrDefaultAsync(x => x.Site.Id == request.Id, cancellationToken)
        ??  throw new NotFoundException(nameof(Site), request.Id);

        if(entity.Site.OwnerId != _currentUserService.UserId)
            throw new ForbiddenAccessException(nameof(Site), request.Id.ToString());

        _context.Elements.RemoveRange(entity.Elements);
        _context.Pages.RemoveRange(entity.Pages);        
        _context.Sites.Remove(entity.Site);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}