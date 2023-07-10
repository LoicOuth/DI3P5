using USite.Domain.Entities;

namespace IntegrationTests.Factories;

public static class SiteFactory
{
    private static Site CreateSite(string userId, int x)  => new(userId, "TEST_SITE_" + x, "DESCRIPTION");

    public static Site GetSite(string userId) => CreateSite(userId, 1);

    public static List<Site> GetSites (string userId, int count)
    {
        var result = new List<Site>();
        for (int i = 0; i < count; i++)
        {
            result.Add(CreateSite(userId, i));
        }
        return result;
    }
}
