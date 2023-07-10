using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Pages.Commands.UpdatePage;
using USite.Domain.Entities;
using static IntegrationTests.Testing;

namespace IntegrationTests.Pages.Commands;

public class UpdatePageCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidPageId()
    {
        _ = await RunAsDefaultUserAsync();
        await FluentActions.Invoking(() => SendAsync(
            new UpdatePageCommand("TestName", "Test", false, Guid.NewGuid())
            )).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdatePage()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        var page = PageFactory.GetPage(site);
        await AddAsync(page);

        var command = new UpdatePageCommand("Updated Page Name", "Description", true, page.Id);
        await SendAsync(command);

        page = await FindAsync<Page>(page.Id);
        page.Should().NotBeNull();
        page!.Name.Should().Be(command.Name);
        page.Description.Should().Be(command.Description);
        page.IsFirst.Should().Be(command.IsFirst);
    }

    [Test]
    public async Task ShouldSetRandomPageAsFirstIfIsFirstSetToFalse()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        var pages = PageFactory.GetPages(site, 3);
        await AddAsync(site);

        var command = new UpdatePageCommand("Updated Page Name", "Description", false, pages[0].Id);
        await SendAsync(command);

        var updatedPage = await FindAsync<Page>(pages[0].Id);
        updatedPage.Should().NotBeNull();
        updatedPage!.Name.Should().Be(command.Name);
        updatedPage.Description.Should().Be(command.Description);
        updatedPage.IsFirst.Should().BeFalse();

        var isFirstPage = await FindAsyncPredicate<Page>(x => x.Site.Id == site.Id && x.IsFirst);
        isFirstPage.Should().NotBeNull();
        isFirstPage!.Id.Should().NotBe(pages[0].Id);
    }

    [Test]
    public async Task ShouldNotSetRandomPageAsFirstIfOnlyOnePageExists()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        var page = PageFactory.GetPage(site);
        await AddAsync(page);

        var command = new UpdatePageCommand("Updated Page Name", "Description", false, page.Id);
        await SendAsync(command);

        var updatedPage = await FindAsync<Page>(page.Id);
        updatedPage.Should().NotBeNull();
        updatedPage!.Name.Should().Be(command.Name);
        updatedPage.Description.Should().Be(command.Description);
        updatedPage.IsFirst.Should().BeFalse();

        var isFirstPage = await FindAsyncPredicate<Page>(x => x.Site.Id == site.Id && x.IsFirst);
        isFirstPage.Should().BeNull();
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new UpdatePageCommand("TestName", "Test", false, Guid.NewGuid())
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}