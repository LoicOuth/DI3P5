namespace USite.Application.Pages.Commands.CreatePage;
public class CreatePageCommandValidator : AbstractValidator<CreatePageCommand>
{
    public CreatePageCommandValidator()
    {
        RuleFor(x => x.SiteId).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}

