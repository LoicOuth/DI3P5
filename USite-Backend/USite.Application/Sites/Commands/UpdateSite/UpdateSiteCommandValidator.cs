namespace USite.Application.Sites.Commands.UpdateSite;

public class UpdateSiteCommandValidator : AbstractValidator<UpdateSiteCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateSiteCommandValidator(IApplicationDbContext context)
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

    public async Task<bool> BeUniqueTitle(UpdateSiteCommand model, string name, CancellationToken cancellationToken)
    {
        return await _context.Sites
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}