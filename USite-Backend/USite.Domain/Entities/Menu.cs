namespace USite.Domain.Entities;

public class Menu : BaseEntity
{
    public Menu()
    {

    }
    public Site Site { get; set; }
    public Guid SiteId { get; set; }
}

