using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Pages.Commands.CreatePage;
using USite.Domain.Entities;
using static IntegrationTests.Testing;

namespace IntegrationTests.Pages.Commands;

public class CreatePageCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidSiteId()
    {
        _ = await RunAsDefaultUserAsync();
        await FluentActions.Invoking(() => SendAsync(
            new CreatePageCommand("TestName", "Test", Guid.NewGuid())
            )).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldHaveAName()
    {
        _ = await RunAsDefaultUserAsync();

        await FluentActions.Invoking(() => SendAsync(
            new CreatePageCommand(string.Empty, "Test", Guid.NewGuid())
            )).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreatePage()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        await AddAsync(site);

        var command = new CreatePageCommand("Test1", "Description", site.Id);
        var pageDto = await SendAsync(command);

        var page = await FindAsync<Page>(pageDto.Id);
        page.Should().NotBeNull();
        page!.Name.Should().Be(command.Name);
        page!.Description.Should().Be(command.Description);
        page!.IsFirst.Should().BeFalse();
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new CreatePageCommand("TestName", "Test", Guid.NewGuid())
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
