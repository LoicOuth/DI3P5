using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace USite.Application.Common.Interfaces;

public interface IAzureDevopsPipelineHelper
{
    Task<Build> CreatePipeline(GitRepository repository, string pipelineName);

    Task<List<Build>> GetLastDeployments(List<string> pipelinesName, int? lastDeploymentCount);
}
