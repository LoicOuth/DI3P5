
using USite.Application.Elements.Commands.UpdateElementPosition;

public class UpdateElementPositionCommandValidator : AbstractValidator<UpdateElementPositionCommand>
{
    public UpdateElementPositionCommandValidator()
    {
        RuleFor(x => x.ElementId).NotNull().NotEmpty();
        RuleFor(x => x.PositionCounter).NotEmpty();
    }
}
