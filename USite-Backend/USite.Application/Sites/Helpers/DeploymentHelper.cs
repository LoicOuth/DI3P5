using Microsoft.TeamFoundation.Build.WebApi;
using USite.Domain.Events;

namespace USite.Application.Sites.Helpers;

public class DeploymentHelper
{
    private readonly IMediator _mediator;

    public DeploymentHelper(IMediator mediatr)
    {
        _mediator = mediatr;
    }

    public async Task DispachNewProgress(Guid siteId, int progress, int step, BuildResult buildResult = BuildResult.None)
    {
        await _mediator.Publish(new ProgressUpdatedEvent(siteId, progress, step, buildResult));
    }
}
