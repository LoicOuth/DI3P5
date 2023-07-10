namespace USite.Application.Common.Models;

public class SiteDeployment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Domain { get; set; }
    public string? SubDomain { get; set; }

    public SiteDeployment(Guid id, string name, string description, string? domain, string? subDomain)
    {
        Id = id;
        Name = name;
        Description = description;
        Domain = domain;
        SubDomain = subDomain;
    }

    public static SiteDeployment Projection(Site site)
        => new(site.Id, site.Name, site.Description, site.Domain, site.SubDomain);
}
