using FluentAssertions;
using IntegrationTests.Factories;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Elements.Queries.GetElementsWithPageIdQuery;
using static IntegrationTests.Testing;

namespace IntegrationTests.Elements.Queries;

public class GetElementsWithPageIdQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidSiteId()
    {
        _ = await RunAsDefaultUserAsync();

        await FluentActions.Invoking(() => SendAsync(
           new GetElementsWithPageIdQuery(Guid.Empty)
           )).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldReturnAllPagesAndElements()
    {
        var userId = await RunAsDefaultUserAsync();
        var site = SiteFactory.GetSite(userId);
        var page = PageFactory.GetPage(site);
        var block = ElementFactory.GetBlock(0, page, new() { ElementFactory.GetImage(0) });
        await AddAsync(site);

        var result = await SendAsync(new GetElementsWithPageIdQuery(page.Id));

        int nombreElement = 0;
        result.Count.Should().Be(1);
        var element = result;
        do
        {
            nombreElement += element.Count;
            element = element.SelectMany(x => x.ElementsChilds!).ToList();
        } while (element.Any());

        nombreElement.Should().Be(2);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new GetElementsWithPageIdQuery(Guid.Empty)
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}