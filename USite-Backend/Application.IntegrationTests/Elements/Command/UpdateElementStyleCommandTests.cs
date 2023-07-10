using FluentAssertions;
using NUnit.Framework;
using USite.Application.Common.Exceptions;
using USite.Application.Elements.Commands.UpdateElementStyle;
using static IntegrationTests.Testing;

namespace IntegrationTests.Elements.Command;
public class UpdateElementStyleCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidSiteId()
    {
        _ = await RunAsDefaultUserAsync();

        await FluentActions.Invoking(() => SendAsync(
           new UpdateElementStyleCommand(Guid.Empty, USite.Domain.Enums.StyleProperty.TextAlign, "NewValue")
           )).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await FluentActions.Invoking(() => SendAsync(
            new UpdateElementStyleCommand(Guid.NewGuid(), USite.Domain.Enums.StyleProperty.TextAlign, "NewValue")
            )).Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
