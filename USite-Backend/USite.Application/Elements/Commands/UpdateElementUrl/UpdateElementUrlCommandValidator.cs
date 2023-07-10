using USite.Application.Elements.Commands.UpdateElementUrl;

public class UpdateElementUrlCommandValidator : AbstractValidator<UpdateElementUrlCommand>
{
    public UpdateElementUrlCommandValidator()
    {
        RuleFor(x => x.ElementId).NotNull().NotEmpty();
        RuleFor(x => x.File).NotEmpty();
    }
}
