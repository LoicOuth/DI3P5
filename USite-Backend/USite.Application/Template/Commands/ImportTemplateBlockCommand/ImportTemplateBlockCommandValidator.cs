namespace USite.Application.Template.Commands.ImportHtmlCommand;

public class ImportTemplateBlockCommandValidator : AbstractValidator<ImportTemplateBlockCommand>
{
    public ImportTemplateBlockCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Html).NotEmpty();
    }
}