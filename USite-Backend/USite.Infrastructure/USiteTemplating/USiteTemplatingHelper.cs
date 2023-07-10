using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;
using USite.Application.Common.Models;
using USite.Infrastructure.Settings;

namespace USite.Infrastructure.USiteTemplating;

public class USiteTemplatingHelper
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<USiteTemplatingHelper> _logger;

    public USiteTemplatingHelper(IConfiguration configuration, ILogger<USiteTemplatingHelper> logger)
    {
        _httpClient = new HttpClient();
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string> GetCi(Guid siteName)
    {
        var content = new JObject
        {
            ["SiteName"] = siteName
        };

        return await SendAsync("ci", content);
    }

    public async Task<string> GetNginxConf(Guid siteName, string dnsName)
    {
        var content = new JObject
        {
            ["DnsName"] = dnsName,
            ["SiteName"] = siteName
        };

        return await SendAsync("nginx", content);
    }

    public async Task<string> GetIngress(Guid siteName, string dnsName)
    {
        var content = new JObject
        {
            ["DnsName"] = dnsName,
            ["SiteName"] = siteName
        };

        return await SendAsync("ingress", content);
    }

    public async Task<string> GetHtml(PageDeployment page)
    {
        var content = new JObject
        {
            ["Page"] = JObject.Parse(JsonConvert.SerializeObject(page))
        };

        return await SendAsync("html", content);
    }

    private async Task<string> SendAsync(string url, JObject content)
    {
        var response = await _httpClient.PostAsync($"{_configuration.GetUSiteTemplatingUrl()}/{url}", new StringContent(content.ToString(), Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Error sending request to USite-Templating API : {error}", response.ReasonPhrase);

            throw new Exception($"HTTP request to '{url}' failed with status code {response.StatusCode}.");
        }

        _logger.LogInformation("New request on USite-Templating API is done : {request}", response.RequestMessage);

        return Regex.Unescape(await response.Content.ReadAsStringAsync()).Trim('"');
    }
}
