using Microsoft.TeamFoundation.Build.WebApi;

namespace USite.Application.Sites.Queries;

public class BuildDto
{
    public string SiteName { get; set; }
    public string SiteUrl { get; set; }
    public DateTime? FinishTime { get; set; }
    public DateTime? StartTime { get; set; }
    public BuildResult? Result { get; set; }
    public BuildStatus? Status { get; set; }

    public BuildDto(string siteName, string siteUrl, DateTime? finishedTime, DateTime? startTime, BuildResult? result, BuildStatus? status)
    {
        SiteName = siteName;
        SiteUrl = siteUrl;
        FinishTime = finishedTime;
        StartTime = startTime;
        Result = result;
        Status = status;
    }

    public static BuildDto Projection(Build build, List<Site> sites)
    {
        var site = sites.First(x => $"Pipeline-{x.Id}".Equals(build.Definition.Name));
       
        return new(site.Name, $"{site.SubDomain}.{site.Domain}", build.FinishTime, build.StartTime, build.Result, build.Status);
    }

    public static List<BuildDto> Projection(List<Build> builds, List<Site> sites)
    {
        var result = new List<BuildDto>();

        builds.ForEach(x => result.Add(Projection(x, sites)));

        return result;
    }
}
