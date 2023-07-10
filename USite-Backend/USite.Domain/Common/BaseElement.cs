using USite.Domain.Entities;

namespace USite.Domain.Common;

public abstract class BaseElement : BaseAuditableEntity
{
    public BaseElement(int position)
    {
        Position = position;
        ElementsChilds = new List<BaseElement>();
        Styles = new List<Style> { };
    }

    public TypeElement Type { get; set; }
    public BaseElement? Parent { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? PageId { get; set; }
    public Guid? MenuId { get; set; }
    public int Position { get; set; }
    public List<BaseElement> ElementsChilds { get; set; }
    public List<Style> Styles { get; set; } 
}
