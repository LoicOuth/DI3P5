using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Pages.Query.GetAllPageWithSiteId;
using static IntegrationTests.Testing;

namespace IntegrationTests.Pages.Queries;

public class GetPagesWithSiteIdQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidSiteId()
    {
        _ = await RunAsDefaultUserAsync();

        await FluentActions.Invoking(() => SendAsync(
           new GetPagesWithSiteIdQuery(Guid.Empty)
           )).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldReturnAllPages()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        _ = PageFactory.GetPages(site, 2);
        await AddAsync(site);

        var result = await SendAsync(new GetPagesWithSiteIdQuery(site.Id));

        result.Count.Should().Be(2);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new GetPagesWithSiteIdQuery(Guid.Empty)
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
