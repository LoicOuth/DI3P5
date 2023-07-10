using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace USite.Application.Common.Interfaces;

public interface IAzureDevopsRepositoryHelper
{
    Task<GitRepository> GetOrCreateRepositoryAsync(string repositoryName);

    Task UploadFilesToRepo(GitRepository repository, SiteDeployment site, List<PageDeployment> elements, string? comment);
}
