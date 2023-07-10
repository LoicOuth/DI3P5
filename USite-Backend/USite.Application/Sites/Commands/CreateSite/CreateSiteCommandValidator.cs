namespace USite.Application.Sites.Commands.CreateSite;

public class CreateSiteCommandValidator : AbstractValidator<CreateSiteCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateSiteCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified name already exists.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
    }

    public async Task<bool> BeUniqueTitle(CreateSiteCommand model, string name, CancellationToken cancellationToken)
    {
        return await _context.Sites
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}