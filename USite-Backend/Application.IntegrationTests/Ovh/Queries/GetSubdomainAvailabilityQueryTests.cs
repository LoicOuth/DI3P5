using FluentAssertions;
using NUnit.Framework;
using USite.Application.Ovh.Queries.GetSubdomainAvailabilityQuery;
using static IntegrationTests.Testing;

namespace IntegrationTests.Ovh.Queries;

public class GetSubdomainAvailabilityQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnUnavailable()
    {
        _ = await RunAsDefaultUserAsync();

        var result = await SendAsync(new GetSubdomainAvailabilityQuery("test2"));

        result.Should().BeTrue();   
    }

    [Test]
    public async Task ShouldReturnAvailable()
    {
        _ = await RunAsDefaultUserAsync();

        var result = await SendAsync(new GetSubdomainAvailabilityQuery("test"));

        result.Should().BeFalse();
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new GetSubdomainAvailabilityQuery(string.Empty)
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}