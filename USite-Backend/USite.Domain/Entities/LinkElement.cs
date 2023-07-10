namespace USite.Domain.Entities;

public class LinkElement : BaseElementWithContent
{
	public LinkElement(string content, int position) : base(content, position)
	{

	}

    public Page Page { get; set; }
}
