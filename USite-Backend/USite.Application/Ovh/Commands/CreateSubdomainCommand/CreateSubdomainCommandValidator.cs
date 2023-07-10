namespace USite.Application.Ovh.Commands.CreateSubdomainCommand;

public class CreateSubdomainCommandValidator : AbstractValidator<CreateSubdomainCommand>
{
    public CreateSubdomainCommandValidator()
    {
        RuleFor(x => x.SiteId).NotNull().NotEmpty();
        RuleFor(x => x.SubDomain).NotNull().NotEmpty();
    }
}