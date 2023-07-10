namespace USite.Application.Pages.Query.Dto;

public class PageDto
{
    public PageDto(Guid id, string name, string description, bool isFirst)
    {
        Id = id;
        Name = name;
        Description = description;
        IsFirst = isFirst;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsFirst { get; set; }

    public static PageDto Projection(Page page)
    {
        return new(page.Id, page.Name, page.Description, page.IsFirst);
    }

    public static List<PageDto> Projection(List<Page> page)
    {
        var result = new List<PageDto>();
        
        page.ForEach(x => result.Add(Projection(x)));

        return result;
    }
}
