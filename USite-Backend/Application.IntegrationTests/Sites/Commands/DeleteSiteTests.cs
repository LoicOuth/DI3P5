using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Sites.Commands.DeleteSite;
using USite.Domain.Entities;
using static IntegrationTests.Testing;

namespace IntegrationTests.Sites.Commands;

public class DeleteSiteTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidSiteId()
    {
        _ = await RunAsDefaultUserAsync();
        
        await FluentActions.Invoking(() => SendAsync(
            new DeleteSiteCommand(Guid.NewGuid())
            )).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteSite()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        await AddAsync(site);

        await SendAsync(new DeleteSiteCommand(site.Id));

        var list = await FindAsync<Site>(site.Id);
        list.Should().BeNull();
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new DeleteSiteCommand(Guid.NewGuid())
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
