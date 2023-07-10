namespace USite.Application.Sites.Commands.UpdateSite;

[Authorize]
public record UpdateSiteCommand(Guid Id, string Name, string Description) : IRequest;

public class UpdateSiteCommandHandler : IRequestHandler<UpdateSiteCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSiteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sites.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Site), request.Id);

        entity.Name = request.Name;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}