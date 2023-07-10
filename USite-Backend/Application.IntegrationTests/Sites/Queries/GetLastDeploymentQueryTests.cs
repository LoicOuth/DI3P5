using FluentAssertions;
using NUnit.Framework;
using USite.Application.Sites.Queries.GetLastDeploymentQuery;
using static IntegrationTests.Testing;

namespace IntegrationTests.Sites.Queries;

public class GetLastDeploymentQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new GetLastDeploymentQuery(5)
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}