namespace USite.Application.Ovh.Queries.GetSubdomainAvailabilityQuery;

[Authorize]
public record GetSubdomainAvailabilityQuery(string SubDomain) : IRequest<bool>;

public class GetSubdomainAvailabilityQueryHadnler : IRequestHandler<GetSubdomainAvailabilityQuery, bool>
{
    private readonly IOvhDomainNameHelper _ovhHelper;
    public GetSubdomainAvailabilityQueryHadnler(IOvhDomainNameHelper ovhHelper)
    {
        _ovhHelper = ovhHelper;
    }

    public async Task<bool> Handle(GetSubdomainAvailabilityQuery request, CancellationToken cancellationToken)
    {
        return await _ovhHelper.CheckSubdomainAvailability(request.SubDomain);
    }
}