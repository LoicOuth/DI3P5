using USite.Application.Elements.Queries.Dto;

namespace USite.Application.Common.Models;

public class PageDeployment
{
    public PageDeployment(Guid id, string name, string description, bool isFirst, List<ElementDto> elements)
    {
        Id = id;
        Name = name;
        Description = description;
        IsFirst = isFirst;
        Elements = elements;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsFirst { get; set; }
    public List<ElementDto> Elements { get; set; }

    public static PageDeployment Projection(Page page, List<ElementDto> elements)
        => new(page.Id, page.Name, page.Description, page.IsFirst, elements);
}