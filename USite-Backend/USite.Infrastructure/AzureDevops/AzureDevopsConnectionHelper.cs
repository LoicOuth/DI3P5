using Microsoft.Extensions.Configuration;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using USite.Infrastructure.Settings;

namespace USite.Infrastructure.AzureDevops;

public class AzureDevopsConnectionHelper
{
	private readonly VssConnection _connection;

    public ProjectHttpClient ProjectHttpClient => _connection.GetClient<ProjectHttpClient>();
    public GitHttpClient GitHttpClient => _connection.GetClient<GitHttpClient>();
    public BuildHttpClient BuildHttpClient => _connection.GetClient<BuildHttpClient>();
    public TaskAgentHttpClient TaskAgentHttpClient => _connection.GetClient<TaskAgentHttpClient>();

    public AzureDevopsConnectionHelper(IConfiguration configuration)
	{
		var creds = new VssBasicCredential(string.Empty, configuration.GetAzureDevopsPAT());
		_connection = new VssConnection(new Uri(AzureDevopsConstants.COLLECTION_URI), creds);
    }
}
