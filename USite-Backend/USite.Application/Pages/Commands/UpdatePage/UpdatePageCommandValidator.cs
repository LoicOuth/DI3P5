namespace USite.Application.Pages.Commands.UpdatePage;

public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
{
    public UpdatePageCommandValidator()
    {
        RuleFor(x => x.PageId).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.IsFirst).NotNull();
    }
}