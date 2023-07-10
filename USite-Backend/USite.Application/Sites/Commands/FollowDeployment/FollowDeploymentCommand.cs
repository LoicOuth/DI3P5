using USite.Application.Sites.Commands.FollowDeployment.Dtos;
using USite.Application.Sites.Helpers;

namespace USite.Application.Sites.Commands.FollowDeployment;

public record FollowDeploymentCommand(string Json) : IRequest;

public class FollowDeploymentCommandHandler : IRequestHandler<FollowDeploymentCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly DeploymentHelper _deploymentHelper;

    public FollowDeploymentCommandHandler(IApplicationDbContext context, DeploymentHelper deploymentHelper)
    {
        _context = context;
        _deploymentHelper = deploymentHelper;
    }

    public async Task<Unit> Handle(FollowDeploymentCommand request, CancellationToken cancellationToken)
    {
        var pipeline = PipelineDto.Projection(request.Json);

        var site = await _context.Sites.FirstOrDefaultAsync(x => x.Id == pipeline.RepositoryName, cancellationToken) ?? throw new NotFoundException(nameof(Site), pipeline.RepositoryName);

        await _deploymentHelper.DispachNewProgress(site.Id, 100, 4, pipeline.Result);

        return Unit.Value;
    }
}
