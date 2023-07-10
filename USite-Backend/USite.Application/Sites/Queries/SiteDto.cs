namespace USite.Application.Sites.Queries;

public class SiteDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Domain { get; set; }
    public string? SubDomain { get; set; }

    public SiteDto(Guid id, string name, string description, string? domain, string? subDomain)
    {
        Id = id;
        Name = name;
        Description = description;
        Domain = domain;
        SubDomain = subDomain;
    }

    public static SiteDto Projection(Site site)
        => new(site.Id, site.Name, site.Description, site.Domain, site.SubDomain);
}