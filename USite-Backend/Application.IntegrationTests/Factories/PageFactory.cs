using USite.Domain.Entities;

namespace IntegrationTests.Factories;

public static class PageFactory
{
    private static Page CreatePage(Site site, int x, bool isFirst = false)
    {
        Page page = new("PAGE_" + x, "DESCRIPTION") { IsFirst = isFirst, Site = site };
        site.Pages.Add(page);
        return page;
    }

    public static Page GetPage(Site site) => CreatePage(site, 1);

    public static List<Page> GetPages(Site site, int count)
    {
        var first = true;
        var result = new List<Page>();
        for (int i = 0; i < count; i++)
        {
            result.Add(CreatePage(site, i, first));
            first = false;
        }
        return result;
    }
}
