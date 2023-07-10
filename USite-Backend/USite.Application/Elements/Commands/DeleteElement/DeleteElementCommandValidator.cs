namespace USite.Application.Elements.Commands.DeleteElement;

public class DeleteElementCommandValidator : AbstractValidator<DeleteElementCommand>
{
    public DeleteElementCommandValidator()
    {
        RuleFor(x => x.ElementId).NotNull().NotEmpty();
    }
}