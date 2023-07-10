using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using USite.Application.Common.Interfaces;
using USite.Infrastructure.Settings;

namespace USite.Infrastructure.Ovh;

public class OvhDomainNameHelper : IOvhDomainNameHelper
{
    private readonly OvhClient _ovhClient;
    private readonly IConfiguration _configuration;
    public OvhDomainNameHelper(OvhClient ovhClient, IConfiguration configuration)
    {
        _ovhClient= ovhClient;
        _configuration= configuration;
    }

    public async Task<bool> CheckSubdomainAvailability(string subdomainName)
    {
        string endPoint = $"domain/zone/{OvhConstants.DOMAIN_NAME}/record?fieldType=A&subDomain={subdomainName}";

        HttpResponseMessage response = await _ovhClient.GetAsync(endPoint);

        string jsonResponse = await response.Content.ReadAsStringAsync();
        JArray records = JArray.Parse(jsonResponse);

        return records.Count == 0;
    }

    public async Task<string> CreateSubDomain(string subdomainName)
    {
        string jsonContent = JsonConvert.SerializeObject(new DnsRecord(subdomainName, _configuration.GetOvhTarget()));

        await _ovhClient.PostAsync($"domain/zone/{OvhConstants.DOMAIN_NAME}/record", jsonContent);

        await _ovhClient.PostAsync($"domain/zone/{OvhConstants.DOMAIN_NAME}/refresh", null);

        return OvhConstants.DOMAIN_NAME;
    }
}

internal class DnsRecord
{
    public string fieldType { get; } = "A";
    public string subDomain { get; }
    public string target { get; }
    public int ttl { get; } = 3600;

    public DnsRecord(string _subDomain, string _target)
    {
        subDomain = _subDomain;
        target = _target;
    }
}
