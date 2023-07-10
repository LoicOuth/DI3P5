namespace IntegrationTests.Elements.Command;
using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Elements.Commands.DeleteElement;
using USite.Domain.Common;
using USite.Domain.Entities;
using static IntegrationTests.Testing;

public class DeleteElementCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidElement()
    {
        _ = await RunAsDefaultUserAsync();

        await FluentActions.Invoking(() => SendAsync(
           new DeleteElementCommand(Guid.Empty)
           )).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new DeleteElementCommand(Guid.NewGuid())
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldDeleteElement()
    {
        var userId = await RunAsDefaultUserAsync();

        var site = SiteFactory.GetSite(userId);
        var page = PageFactory.GetPage(site);
        var element = ElementFactory.GetBlock(0, page, new List<BaseElement>());
        await AddAsync(element);

        var command = new DeleteElementCommand(element.Id);
        await SendAsync(command);

        element = await FindAsync<BlockElement>(element.Id);
        element.Should().BeNull();
    }
}
