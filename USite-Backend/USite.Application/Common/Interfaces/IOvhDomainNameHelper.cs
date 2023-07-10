namespace USite.Application.Common.Interfaces;

public interface IOvhDomainNameHelper
{
    Task<bool> CheckSubdomainAvailability(string subdomainName);
    Task<string> CreateSubDomain(string subdomainName);
}
