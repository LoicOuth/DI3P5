namespace USite.Application.Elements.Commands.UpdateElementStyle;

public class UpdateElementStyleCommandValidator : AbstractValidator<UpdateElementStyleCommand>
{
	public UpdateElementStyleCommandValidator()
	{
		RuleFor(x => x.ElementId).NotNull().NotEmpty();
		RuleFor(x => x.Property).IsInEnum();
		RuleFor(x => x.Value).Must(IsValidNotEmptyOrNull);
    }

	private bool IsValidNotEmptyOrNull(string value)
	{
		if (value == null)
			return true;

		if(value != null && !string.IsNullOrWhiteSpace(value))
			return true;

		return false;
	}
}
