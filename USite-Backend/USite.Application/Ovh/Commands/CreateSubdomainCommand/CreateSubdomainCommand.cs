namespace USite.Application.Ovh.Commands.CreateSubdomainCommand;

[Authorize]
public record CreateSubdomainCommand(Guid SiteId, string SubDomain) : IRequest<Unit>;

public class CreateSubdomainCommandHandler : IRequestHandler<CreateSubdomainCommand, Unit>
{
    private readonly IOvhDomainNameHelper _ovhHelper;
    private readonly IApplicationDbContext _context;

    public CreateSubdomainCommandHandler(IOvhDomainNameHelper ovhHelper, IApplicationDbContext context)
    {
        _ovhHelper = ovhHelper;
        _context = context;
    }

    public async Task<Unit> Handle(CreateSubdomainCommand request, CancellationToken cancellationToken)
    {
        var site = await _context.Sites.FirstOrDefaultAsync(x => x.Id == request.SiteId, cancellationToken) ?? throw new NotFoundException("Site", request.SiteId);

        var domain = await _ovhHelper.CreateSubDomain(request.SubDomain);

        site.Domain = domain;
        site.SubDomain = request.SubDomain;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}