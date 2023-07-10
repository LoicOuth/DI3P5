using Microsoft.TeamFoundation.Build.WebApi;

namespace USite.Application.Common.Models;

public class ProgressModel
{
    public int Progress { get; set; }
    public int Step { get; set; }
    public BuildResult Result { get; set; }

    public ProgressModel(int progress, int step, BuildResult result = BuildResult.None)
    {
        Progress = progress;
        Step = step;
        Result = result;
    }
}
