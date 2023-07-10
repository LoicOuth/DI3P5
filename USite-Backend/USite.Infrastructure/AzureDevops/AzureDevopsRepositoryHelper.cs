
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using USite.Application.Common.Interfaces;
using USite.Application.Common.Models;
using USite.Infrastructure.USiteTemplating;

namespace USite.Infrastructure.AzureDevops;

public class AzureDevopsRepositoryHelper : IAzureDevopsRepositoryHelper
{
    public readonly ILogger<AzureDevopsRepositoryHelper> _logger;
    private readonly AzureDevopsConnectionHelper _connection;
    private readonly USiteTemplatingHelper _templatingHelper;

    public AzureDevopsRepositoryHelper(ILogger<AzureDevopsRepositoryHelper> logger, AzureDevopsConnectionHelper connection, USiteTemplatingHelper templatingHelper)
    {
        _connection = connection;
        _logger = logger;
        _templatingHelper = templatingHelper;
    }

    public async Task<GitRepository> GetOrCreateRepositoryAsync(string repositoryName)
    {

        var existingRepositories = await _connection.GitHttpClient.GetRepositoriesAsync(AzureDevopsConstants.DEFAULT_PROJECT_NAME);
        var existingRepository = existingRepositories.FirstOrDefault(r => r.Name.Equals(repositoryName, StringComparison.OrdinalIgnoreCase));

        if (existingRepository != null) return existingRepository;

        var project = await _connection.ProjectHttpClient.GetProject(AzureDevopsConstants.DEFAULT_PROJECT_NAME) ?? throw new InvalidOperationException("Project");
        var newRepo = new GitRepositoryCreateOptions()
        {
            Name = repositoryName,
            ProjectReference = new TeamProjectReference()
            {
                Id = project.Id,
                Name = project.Name
            }
        };

        var createdRepository = await _connection.GitHttpClient.CreateRepositoryAsync(newRepo);

        _logger.LogInformation("New repository created {repoName}", createdRepository.Name);

        return createdRepository;
    }

    public async Task UploadFilesToRepo(GitRepository repository, SiteDeployment site, List<PageDeployment> pages, string? comment)
    {
        var project = await _connection.ProjectHttpClient.GetProject(AzureDevopsConstants.DEFAULT_PROJECT_NAME);
        var changes = new List<GitChange>();

        foreach (var page in pages)
        {
            string fileName = page.IsFirst ? "index.html" : $"{page.Name}.html";

            GitItem? existingFile = null;
            try
            {
                existingFile = await _connection.GitHttpClient.GetItemAsync(project.Id, repository.Id, fileName, includeContentMetadata: true);
            }
            catch (Exception)
            {

            }

            var fileContent = await _templatingHelper.GetHtml(page);

            var change = new GitChange
            {
                ChangeType = existingFile != null ? VersionControlChangeType.Edit : VersionControlChangeType.Add,
                Item = new GitItem { Path = fileName },
                NewContent = new ItemContent
                {
                    ContentType = ItemContentType.RawText,
                    Content = fileContent
                }
            };

            changes.Add(change);
        }

        var mainBranch = await _connection.GitHttpClient.GetRefsAsync(project.Id, repository.Id, filter: AzureDevopsConstants.DEFAULT_BRANCH);
        string? mainBranchOldObjectId = mainBranch.FirstOrDefault()?.ObjectId;

        var ciPath = $"/{AzureDevopsConstants.CI_FILE_NAME}";
        var nginxConfPath = $"/{site.Id}.conf";
        var ingressPath = $"/{AzureDevopsConstants.INGRESS_FILE_NAME}";

        //Get ci
        AddChange(mainBranchOldObjectId, ciPath, await _templatingHelper.GetCi(site.Id));
        //Get nginx
        AddChange(mainBranchOldObjectId, nginxConfPath, await _templatingHelper.GetNginxConf(site.Id, $"{site.SubDomain}.{site.Domain}"));
        //Get ingress
        AddChange(mainBranchOldObjectId, ingressPath, await _templatingHelper.GetIngress(site.Id, $"{site.SubDomain}.{site.Domain}"));

        void AddChange(string? mainBranchOldId, string path, string content)
        {
            var changeType = mainBranchOldId != null ? VersionControlChangeType.Edit : VersionControlChangeType.Add;
            changes.Add(new GitChange
            {
                ChangeType = changeType,
                Item = new GitItem { Path = path },
                NewContent = new ItemContent { ContentType = ItemContentType.RawText, Content = content }
            });
        }

        mainBranchOldObjectId ??= "0000000000000000000000000000000000000000";


        var gitPush = new GitPush
        {
            RefUpdates = new List<GitRefUpdate>
                {
                    new GitRefUpdate
                    {
                        Name = $"refs/{AzureDevopsConstants.DEFAULT_BRANCH}",
                        OldObjectId = mainBranchOldObjectId
                    }
                },
            Commits = new List<GitCommitRef>
                {
                    new GitCommitRef
                    {
                        Comment = comment ?? "New deployment",
                        Changes = changes
                    }
                }
        };

        await _connection.GitHttpClient.CreatePushAsync(gitPush, project.Id, repository.Id);

        _logger.LogInformation("New file pushed to the repo {repoId} with comment {comment}", repository.Id, comment);
    }
}
