using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Sites.Commands.UpdateSite;
using USite.Domain.Entities;
using static IntegrationTests.Testing;

namespace IntegrationTests.Sites.Commands;

public class UpdateSiteTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidSiteId()
    {
        _ = await RunAsDefaultUserAsync();
        await FluentActions.Invoking(() => SendAsync(
            new UpdateSiteCommand(Guid.NewGuid(), "TestName", "Test")
            )).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSites(userId, 2);
        await AddAsync(site.First());
        await AddAsync(site[1]);

        var command = new UpdateSiteCommand(site.First().Id, site[1].Name, "Description");

        await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateSite()
    {

        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        await AddAsync(site);

        var command = new UpdateSiteCommand(site.Id, "Updated Site Title", "Description");
        await SendAsync(command);

        site = await FindAsync<Site>(site.Id);
        site.Should().NotBeNull();
        site!.Name.Should().Be(command.Name);
        site.LastModifiedBy.Should().NotBeNull();
        site.LastModifiedBy.Should().Be(userId);
        site.LastModified.Should().NotBeNull();
        site.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new UpdateSiteCommand(Guid.NewGuid(), "TestName", "Test")
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
