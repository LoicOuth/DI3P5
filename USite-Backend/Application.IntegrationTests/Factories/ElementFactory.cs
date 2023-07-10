using USite.Domain.Common;
using USite.Domain.Entities;

namespace IntegrationTests.Factories;

public static class ElementFactory
{
    private static ImageElement CreateImage(int position) => new(position, "TEST_URL", "TEST_ALT");
    private static BlockElement CreateBlock(int position, int x, Page page, List<BaseElement>? elementsChild)
    {
        BlockElement element = new("BLOCK_TEST_" + x, "DESCRITPION", position)
        {
            Page = page
        };

        page?.Elements.Add(element);
        if (elementsChild != null) { element.ElementsChilds = elementsChild; elementsChild.ForEach(x => x.Parent = element); };
        return element;
    }

    public static ImageElement GetImage(int position) => CreateImage(position);

    public static List<ImageElement> GetImages(int position, int count)
    {
        var result = new List<ImageElement>();
        for (int i = 0; i < count; i++)
        {
            result.Add(CreateImage(position));
        }
        return result;
    }

    public static BlockElement GetBlock(int position, Page page, List<BaseElement>? elementsChild) => CreateBlock(position, 1, page, elementsChild);

    public static List<BlockElement> GetBlocks(int position, int count, Page page, List<BaseElement>? elementsChild)
    {
        var result = new List<BlockElement>();
        for (int i = 0; i < count; i++)
        {
            result.Add(CreateBlock(position, i, page, elementsChild));
        }
        return result;
    }
}
