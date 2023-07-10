using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Ovh.Commands.CreateSubdomainCommand;
using USite.Domain.Entities;
using static IntegrationTests.Testing;

namespace IntegrationTests.Ovh.Commands;

public class CreateSubdomainCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidSiteId()
    {
        _ = await RunAsDefaultUserAsync();

        await FluentActions.Invoking(() => SendAsync(
           new CreateSubdomainCommand(Guid.Empty, string.Empty)
           )).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new CreateSubdomainCommand(Guid.NewGuid(), string.Empty)
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldExistingSite()
    {
        _ = await RunAsDefaultUserAsync();

        await FluentActions.Invoking(() => SendAsync(
           new CreateSubdomainCommand(Guid.NewGuid(), "test")
           )).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateSiteAndCreateSubdomain()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        await AddAsync(site);

        var command = new CreateSubdomainCommand(site.Id, "test");
        await SendAsync(command);

        site = await FindAsync<Site>(site.Id);
        site.Should().NotBeNull();
        site.Domain.Should().Be("usite.fr");
        site.SubDomain.Should().Be("test");
    }
}