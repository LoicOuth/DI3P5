namespace USite.Domain.Entities;

public class ImageElement : BaseElement
{
	public ImageElement(int position, string url, string alt):base(position)
	{
		Url = url;
		Alt = alt;
	}

	public string Url { get; set; }
	public string Alt { get; set; }
}
