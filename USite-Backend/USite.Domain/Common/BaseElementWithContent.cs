namespace USite.Domain.Common;

public class BaseElementWithContent : BaseElement
{
    public BaseElementWithContent(string content, int position) : base(position)
    {
        Content = content;

    }

    public string Content { get; set; }
}