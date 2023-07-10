namespace USite.Domain.Entities;

public class Page : BaseEntity
{
    public Page(string name, string description)
    {
        Name = name;
        Description = description;
        IsFirst = false;
        Elements = new ();
    }

    public Site Site { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsFirst { get; set; }
    public List<BlockElement> Elements { get; set; }
}
