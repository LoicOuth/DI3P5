using USite.Domain.Common;
using USite.Domain.Enums;

namespace USite.Application.Elements.Queries.Dto;

public class ElementDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? PageId { get; set; }
    public string? PageName { get; set; }
    public Guid? MenuId { get; set; }
    public TypeElement Type { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public string? Url { get; set; }
    public string? Alt { get; set; }
    public List<ElementDto>? ElementsChilds { get; set; }
    public List<StyleDto> Styles { get; set; }

    public ElementDto(Guid id, TypeElement type, List<ElementDto> elements, List<StyleDto> styles)
    {
        Id = id;
        Type = type;
        ElementsChilds = elements;
        Styles = styles;
    }

    public ElementDto(Guid id, Guid? parentId, Guid? pageId, TypeElement type, List<ElementDto> elements, List<StyleDto> styles, string? name, string? description, Guid? menuId)
    {
        Id = id;
        ParentId = parentId;
        PageId = pageId;
        Type = type;
        ElementsChilds = elements;
        Name = name;
        Description = description;
        Styles = styles;
        MenuId = menuId;
    }

    public ElementDto(Guid id, Guid? parentId, Guid? pageId, TypeElement type, List<ElementDto> elements, List<StyleDto> styles, string? url, string? alt)
    {
        Id = id;
        ParentId = parentId;
        PageId = pageId;
        Type = type;
        ElementsChilds = elements;
        Url = url;
        Alt = alt;
        Styles = styles;
    }

    public ElementDto(Guid id, Guid? parentId, Guid? pageId, string? pageName, TypeElement type, List<ElementDto> elements, List<StyleDto> styles, string? content)
    {
        Id = id;
        ParentId = parentId;
        PageId = pageId;
        PageName = pageName;
        Type = type;
        ElementsChilds = elements;
        Content = content;
        Styles = styles;
    }

    public static ElementDto Projection(BlockElement element)
    {
        return new ElementDto(element.Id, element.ParentId, element.PageId, element.Type, Projection(element.ElementsChilds.ToList()), StyleDto.Projection(element.Styles), element.Name, element.Description, element.MenuId);
    }

    public static List<ElementDto> Projection(List<BlockElement> elements)
    {
        var result = new List<ElementDto>();

        elements.ForEach(x => result.Add(Projection(x)));

        return result;
    }

    public static ElementDto Projection(H1Element element)
    {
        return new ElementDto(element.Id, element.ParentId, null, null, element.Type, Projection(element.ElementsChilds.ToList()), StyleDto.Projection(element.Styles), element.Content);
    }

    public static List<ElementDto> Projection(List<H1Element> elements)
    {
        var result = new List<ElementDto>();

        elements.ForEach(x => result.Add(Projection(x)));

        return result;
    }

    public static ElementDto Projection(ButtonElement element)
    {
        return new ElementDto(element.Id, element.ParentId, null, null, element.Type, Projection(element.ElementsChilds.ToList()), StyleDto.Projection(element.Styles), element.Content);
    }

    public static List<ElementDto> Projection(List<ButtonElement> elements)
    {
        var result = new List<ElementDto>();

        elements.ForEach(x => result.Add(Projection(x)));

        return result;
    }

    public static ElementDto Projection(LinkElement element)
    {
        return new ElementDto(element.Id, element.ParentId, element.PageId, element.Page != null ? element.Page.Name : null, element.Type, Projection(element.ElementsChilds.ToList()), StyleDto.Projection(element.Styles), element.Content);
    }

    public static List<ElementDto> Projection(List<LinkElement> elements)
    {
        var result = new List<ElementDto>();

        elements.ForEach(x => result.Add(Projection(x)));

        return result;
    }

    public static ElementDto Projection(BaseElement element)
    {
        switch(element.Type)
        {
            case TypeElement.H1:
                return Projection((H1Element)element);
            case TypeElement.Block:
                return Projection((BlockElement)element);
            case TypeElement.Button:
                return Projection((ButtonElement)element);
            case TypeElement.Link:
                return Projection((LinkElement)element);
            case TypeElement.Image:
                return Projection((ImageElement)element);
            default:
                return new ElementDto(element.Id, element.Type, Projection(element.ElementsChilds.ToList()), StyleDto.Projection(element.Styles));
        }
    }

    public static ElementDto Projection(ImageElement element)
    {
        return new ElementDto(element.Id, element.ParentId, null, element.Type, Projection(element.ElementsChilds.ToList()), StyleDto.Projection(element.Styles), element.Url, element.Alt);
    }

    public static List<ElementDto> Projection(List<ImageElement> elements)
    {
        var result = new List<ElementDto>();

        elements.ForEach(x => result.Add(Projection(x)));

        return result;
    }

    public static List<ElementDto> Projection(List<BaseElement> elements)
    {
        var result = new List<ElementDto>();

        elements.ForEach(x =>
        {
            switch(x.Type)
            {
                case TypeElement.H1 :

                    result.Add(Projection((H1Element)x));

                    break;

                case TypeElement.Block: 
                    
                    result.Add(Projection((BlockElement)x));
                    break;

                case TypeElement.Button:

                    result.Add(Projection((ButtonElement)x));

                    break;

                case TypeElement.Link:

                    result.Add(Projection((LinkElement)x));

                    break;

                default:
                    result.Add(Projection(x));
                    break;

            }
        });

        return result;
    }
}
