namespace USite.Application.Sites.Commands.NewDeployment;

public class NewDeploymentCommandValidator : AbstractValidator<NewDeploymentCommand>
{
    public NewDeploymentCommandValidator()
    {
        RuleFor(x => x.SiteId).NotEmpty();
    }
}
