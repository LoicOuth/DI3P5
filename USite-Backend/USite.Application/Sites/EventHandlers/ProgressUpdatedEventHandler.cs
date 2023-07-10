using USite.Domain.Events;

namespace USite.Application.Sites.EventHandlers;

public class ProgressUpdatedEventHandler : INotificationHandler<ProgressUpdatedEvent>
{
    private readonly IHubDeploymentContext _hubContext;

    public ProgressUpdatedEventHandler(IHubDeploymentContext context)
    {
        _hubContext = context;
    }

    public async Task Handle(ProgressUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _hubContext.OnProgress(
            notification.SiteId.ToString(),
            new ProgressModel(notification.Progress, notification.Step, notification.Result)
        );
    }
}
