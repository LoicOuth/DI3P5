using Microsoft.TeamFoundation.Build.WebApi;
using System.Text.Json;

namespace USite.Application.Sites.Commands.FollowDeployment.Dtos;

public class PipelineDto
{
    public Guid RepositoryName { get; }
    public BuildResult Result { get; }

    public PipelineDto(Guid repositoryName, BuildResult result)
    {
        RepositoryName = repositoryName;
        Result = result;
    }

    public static PipelineDto Projection(string json)
    {
        var jsonObject = JsonDocument.Parse(json).RootElement;
        var resourceObject = jsonObject.GetProperty("resource");

        var result = resourceObject.GetProperty("result").GetString();
        var repositoryName = resourceObject.GetProperty("repository").GetProperty("name").GetGuid();

        if (result == null)
            throw new InvalidOperationException("No result or Status");

        if (!Enum.TryParse(result, true, out BuildResult resultEnum))
            throw new Exception("Error during parsing enums");

        return new(repositoryName, resultEnum);
    }
}

