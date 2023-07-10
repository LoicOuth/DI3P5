using USite.Domain.Enums;

namespace USite.Application.Elements.Queries.Dto;

public class StyleDto
{
    public StyleDto(StyleProperty property, string value)
    {
        Property = property;
        Value = value;
    }

    public StyleProperty Property { get; set; }
    public string Value { get; set; }

    public static StyleDto Projection(Style style)
    {
        return new StyleDto(style.Property, style.Value);
    }

    public static List<StyleDto> Projection(List<Style> styles)
    {
        var result = new List<StyleDto>();

        styles.ForEach(x => result.Add(Projection(x)));

        return result;
    }
}
