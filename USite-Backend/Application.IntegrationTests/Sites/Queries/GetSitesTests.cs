using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Sites.Queries.GetSites;
using static IntegrationTests.Testing;

namespace IntegrationTests.Sites.Queries;

public class GetSitesTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnList()
    {
        _ = await RunAsDefaultUserAsync();

        var result = await SendAsync(new GetSitesQuery());

        result.Should().NotBeNull();
        result.Count.Should().Be(0);
    }

    [Test]
    public async Task ShouldReturnAllSites()
    {
        var userId = await RunAsDefaultUserAsync();
        var sites = SiteFactory.GetSites(userId, 2);
        sites.ForEach(async x => await AddAsync(x));

        var result = await SendAsync(new GetSitesQuery());

        result.Count.Should().Be(2);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new GetSitesQuery()
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
