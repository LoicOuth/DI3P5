namespace USite.Application.Elements.Commands.UpdateElementContent;

public class UpdateElementContentCommandValidator : AbstractValidator<UpdateElementContentCommand>
{
    public UpdateElementContentCommandValidator()
    {
        RuleFor(x => x.ElementId).NotNull().NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
    }
}
