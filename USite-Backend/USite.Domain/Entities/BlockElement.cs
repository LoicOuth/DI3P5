namespace USite.Domain.Entities;

public class BlockElement : BaseElement
{
    public BlockElement(string name, string description, int position) : base(position)
    {
        Name = name;
        Description = description;
    }
    public Page? Page { get; set; }
    public Menu? Menu { get; set; }
    public bool IsTemplate { get; set; } = false;
    public string Name { get; set; }
    public string Description { get; set; }
}