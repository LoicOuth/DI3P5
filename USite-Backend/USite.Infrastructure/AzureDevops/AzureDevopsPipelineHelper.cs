
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using USite.Application.Common.Interfaces;

namespace USite.Infrastructure.AzureDevops;

public class AzureDevopsPipelineHelper : IAzureDevopsPipelineHelper
{
    public readonly ILogger<AzureDevopsPipelineHelper> _logger;
    private readonly AzureDevopsConnectionHelper _connection;

    public AzureDevopsPipelineHelper(ILogger<AzureDevopsPipelineHelper> logger, AzureDevopsConnectionHelper connection)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<Build> CreatePipeline(GitRepository repository, string pipelineName)
    {
        pipelineName = $"Pipeline-{pipelineName}";
        var project = await _connection.ProjectHttpClient.GetProject(AzureDevopsConstants.DEFAULT_PROJECT_NAME);
        var existingPipelines = await _connection.BuildHttpClient.GetDefinitionsAsync(project.Name);
        var existingPipeline = existingPipelines.FirstOrDefault(p => p.Name.Equals(pipelineName, StringComparison.OrdinalIgnoreCase));

        if (existingPipeline != null) return await ExecutePipeline(project, existingPipeline.Id);

        var pools = await _connection.TaskAgentHttpClient.GetAgentQueuesAsync(project.Id);
        var selectedPool = pools.Where(x => x.Name.Equals("azure pipelines", StringComparison.OrdinalIgnoreCase)).FirstOrDefault() ?? throw new InvalidOperationException("Can't find Azure Pipelines pool");

        var agentPoolQueue = new AgentPoolQueue
        {
            Id = selectedPool.Id,
            Name = selectedPool.Name,
            Pool = new TaskAgentPoolReference()
            {
                Id = selectedPool.Pool.Id,
                Name = selectedPool.Pool.Name,
                IsHosted = selectedPool.Pool.IsHosted,
            }
        };

        var pipelineDefinition = new BuildDefinition
        {
            Name = pipelineName,
            Path = "\\",
            Project = new TeamProjectReference { Name = project.Name, Id = project.Id },
            Process = new YamlProcess
            {
                YamlFilename = AzureDevopsConstants.CI_FILE_NAME,
            },
            Queue = agentPoolQueue,
            Repository = new BuildRepository { Id = repository.Id.ToString(), Type = "TfsGit", DefaultBranch = $"refs/{AzureDevopsConstants.DEFAULT_BRANCH}" }
        };

        var createdPipelineDefinition = await _connection.BuildHttpClient.CreateDefinitionAsync(pipelineDefinition, project.Name);

        _logger.LogInformation("New pipeline created {pipelineName}", pipelineName);

        return await ExecutePipeline(project, createdPipelineDefinition.Id);
    }

    public async Task<List<Build>> GetLastDeployments(List<string> pipelinesName, int? lastDeploymentCount)
    {
        for (int i = 0; i < pipelinesName.Count; i++)
        {
            pipelinesName[i] = $"Pipeline-{pipelinesName[i]}";
        }

        var project = await _connection.ProjectHttpClient.GetProject(AzureDevopsConstants.DEFAULT_PROJECT_NAME);

        var allPipelines = await _connection.BuildHttpClient.GetDefinitionsAsync(project.Name);
        var availablePipelines = allPipelines.Where(x => pipelinesName.Contains(x.Name)).Select(x => x.Id).ToList();

        if (availablePipelines.Count <= 0)
            throw new InvalidOperationException("Pipeline with name " + pipelinesName);

        var buildExecutions = await _connection.BuildHttpClient.GetBuildsAsync(project.Id, availablePipelines, top: lastDeploymentCount ?? 5, queryOrder: BuildQueryOrder.StartTimeDescending);

        return buildExecutions ?? new ();
    }

    private async Task<Build> ExecutePipeline(TeamProject project, int pipelineId)
    {
        var buildToQueue = new Build
        {
            Definition = new DefinitionReference { Id = pipelineId },
            Project = new TeamProjectReference { Id = project.Id, Name = project.Name }
        };

        _logger.LogInformation("New pipeline {pipelineName} start", pipelineId);

        return await _connection.BuildHttpClient.QueueBuildAsync(buildToQueue, project.Name);
    }
}
