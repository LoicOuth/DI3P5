namespace USite.Application.Menus.Query.Dto;

public class MenuDto
{
    public MenuDto(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
    public Guid SiteId { get; set; }

    public static MenuDto Projection(Menu menu)
    {
        return new(menu.Id)
        {
            SiteId = menu.SiteId
        };
    }
}
