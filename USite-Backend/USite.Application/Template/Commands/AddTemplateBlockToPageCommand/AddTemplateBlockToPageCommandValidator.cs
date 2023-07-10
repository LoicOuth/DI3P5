namespace USite.Application.Template.Commands.AddTemplateBlockInSiteCommand;

public class AddTemplateBlockToPageCommandValidator : AbstractValidator<AddTemplateBlockToPageCommand>
{
    public AddTemplateBlockToPageCommandValidator()
    {
        RuleFor(x => x.TemplateId).NotEmpty();
        RuleFor(x => x.PageId).NotEmpty();
    }
}
