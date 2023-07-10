

using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Sites.Queries.GetSiteFromIdQuery;
using static IntegrationTests.Testing;


namespace IntegrationTests.Sites.Queries;

public class GetSiteFromIdQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidSiteId()
    {
        _ = await RunAsDefaultUserAsync();

        await FluentActions.Invoking(() => SendAsync(
           new GetSiteFromIdQuery(Guid.NewGuid())
           )).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldReturnSite()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        await AddAsync(site);

        var result = await SendAsync(new GetSiteFromIdQuery(site.Id));
        result.Should().NotBeNull();
        result!.Id.Should().Be(site.Id);
        result.Name.Should().Be(site.Name);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new GetSiteFromIdQuery(Guid.Empty)
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
