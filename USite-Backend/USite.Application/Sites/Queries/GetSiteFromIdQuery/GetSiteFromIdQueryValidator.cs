
namespace USite.Application.Sites.Queries.GetSiteFromIdQuery;

public class GetSiteFromIdQueryValidator : AbstractValidator<GetSiteFromIdQuery>
{
    public GetSiteFromIdQueryValidator()
    {
        RuleFor(x => x.SiteId).NotNull().NotEmpty();
    }
}