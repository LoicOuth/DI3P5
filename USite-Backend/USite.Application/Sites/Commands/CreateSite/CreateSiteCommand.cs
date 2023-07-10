namespace USite.Application.Sites.Commands.CreateSite;

[Authorize]
public record CreateSiteCommand(string Name, string Description) : IRequest<Guid>;

public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateSiteCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        var entity = new Site(_currentUserService.UserId!, request.Name, request.Description);
        var page = new Page("Home", "Home Page")
        {
            Site = entity,
            IsFirst = true
        };
        
        _context.Pages.Add(page);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}