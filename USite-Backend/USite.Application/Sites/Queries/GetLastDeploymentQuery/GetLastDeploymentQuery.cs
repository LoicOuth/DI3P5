namespace USite.Application.Sites.Queries.GetLastDeploymentQuery;

[Authorize]
public record GetLastDeploymentQuery(int? LastDeploymentCount) : IRequest<List<BuildDto>>;

public class GetLastDeploymentQueryHandler : IRequestHandler<GetLastDeploymentQuery, List<BuildDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IAzureDevopsPipelineHelper _pipelineHelper;
    private readonly ICurrentUserService _userService;

    public GetLastDeploymentQueryHandler(IApplicationDbContext context, IAzureDevopsPipelineHelper pipelineHelper, ICurrentUserService userService)
    {
        _context = context;
        _pipelineHelper = pipelineHelper;
        _userService = userService;
    }

    public async Task<List<BuildDto>> Handle(GetLastDeploymentQuery request, CancellationToken cancellationToken)
    {
        var sites = await _context.Sites.Where(x => x.OwnerId == _userService.UserId).ToListAsync(cancellationToken);

        if (sites.Count > 0)
        {
            var builds = await _pipelineHelper.GetLastDeployments(
                sites.Select(x => x.Id.ToString()).ToList(),
                request.LastDeploymentCount
            );

            return BuildDto.Projection(builds, sites);
        }

        return new();
    }
}
