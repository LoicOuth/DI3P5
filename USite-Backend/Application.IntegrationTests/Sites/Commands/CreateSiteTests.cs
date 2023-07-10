using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Sites.Commands.CreateSite;
using USite.Domain.Entities;
using static IntegrationTests.Testing;

namespace IntegrationTests.Sites.Commands;

public class CreateSiteTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        _ = await RunAsDefaultUserAsync();
        
        await FluentActions.Invoking(() => SendAsync(
            new CreateSiteCommand("", "")
            )).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueName()
    {
        var userId = await RunAsDefaultUserAsync();
        await AddAsync(SiteFactory.GetSite(userId));

        await FluentActions.Invoking(() =>
            SendAsync(
                new CreateSiteCommand("TEST_SITE_1", "Description")
                )).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateSite()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateSiteCommand("Test1", "Description");
        var id = await SendAsync(command);
        
        var site = await FindAsync<Site>(id);
        site.Should().NotBeNull();
        site!.Name.Should().Be(command.Name);
        site.CreatedBy.Should().Be(userId);
        site.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() =>
            SendAsync(
                new CreateSiteCommand("Test1", "Description")
                )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
