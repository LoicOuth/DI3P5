namespace USite.Application.Common.Interfaces;

public interface IHubDeploymentContext
{
    Task OnProgress(string groupName, ProgressModel progress);
}
