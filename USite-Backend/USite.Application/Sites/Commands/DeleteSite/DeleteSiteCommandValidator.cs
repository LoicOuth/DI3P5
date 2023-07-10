namespace USite.Application.Sites.Commands.DeleteSite;

public class DeleteSiteCommandValidator : AbstractValidator<DeleteSiteCommand>
{
	public DeleteSiteCommandValidator()
	{
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("SiteId is required.");
    }
}