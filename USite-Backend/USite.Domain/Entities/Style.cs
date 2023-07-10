namespace USite.Domain.Entities;

public class Style : BaseEntity
{
    public Style(StyleProperty property, string value)
    {
        Property = property;
        Value = value;
    }

    public StyleProperty Property { get; set; }
    public string Value { get; set; }
    public Guid? BaseElementId { get; set; }
}
