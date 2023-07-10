using Microsoft.TeamFoundation.Build.WebApi;

namespace USite.Domain.Events;

public class ProgressUpdatedEvent : BaseEvent
{
    public Guid SiteId { get; set; }
    public int Progress { get; set; }
    public int Step { get; set; }
    public BuildResult Result { get; set; } = BuildResult.None;

    public ProgressUpdatedEvent(Guid siteId, int progress, int step)
    {
        SiteId = siteId;
        Progress = progress;
        Step = step;
    }

    public ProgressUpdatedEvent(Guid siteId, int progress, int step, BuildResult result)
    {
        SiteId = siteId;
        Progress = progress;
        Step = step;
        Result = result;
    }
}
