namespace USite.Domain.Entities;

public class Site : BaseAuditableEntity
{
    public Site(string ownerId, string name, string description)
    {
        OwnerId = ownerId;
        Name = name;
        Description = description;
        Pages = new ();
    }

    public Site(string ownerId, string name, string description, string domain, string subDomain)
    {
        OwnerId = ownerId;
        Name = name;
        Description = description;
        Domain = domain;
        SubDomain = subDomain;
        Pages = new();
    }

    public string OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Domain { get; set; }
    public string? SubDomain { get; set; }
    public List<Page> Pages { get; set; }
}