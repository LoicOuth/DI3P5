using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using USite.Infrastructure.Settings;

namespace USite.Infrastructure.Ovh;

public class OvhClient
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<OvhClient> _logger;
    private readonly HttpClient _httpClient;
    public OvhClient(IConfiguration configuration, ILogger<OvhClient> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _httpClient = new HttpClient();
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        return await SendRequestAsync(HttpMethod.Get, $"{OvhConstants.BASE_URL}{endpoint}");
    }

    public async Task<HttpResponseMessage> PostAsync(string endpoint, string? content)
    {
        return await SendRequestAsync(HttpMethod.Post, $"{OvhConstants.BASE_URL}{endpoint}", content);
    }

    private async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string endpoint, string? content = null)
    {
        string timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        string signature = ComputeSignature(method.Method, endpoint, content, timeStamp);


        HttpRequestMessage request = new(method, endpoint);
        request.Headers.Add("X-Ovh-Application", _configuration.GetOvhApiKey());
        request.Headers.Add("X-Ovh-Timestamp", timeStamp);
        request.Headers.Add("X-Ovh-Signature", signature);
        request.Headers.Add("X-Ovh-Consumer", _configuration.GetOvhConsumerKey());

        if (method == HttpMethod.Post && content != null)
        {
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");
        }

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {

            _logger.LogError("Error sending request : {error}", response.ReasonPhrase);
            throw new Exception("Error sending request");
        }

        _logger.LogInformation("New request on OVH is done : {request}", response.RequestMessage);

        return response;

    }

    private string ComputeSignature(string method, string query, string? body, string timeStamp)
    {
        string rawSignature = $"{_configuration.GetOvhApiSecret()}+{_configuration.GetOvhConsumerKey()}+{method}+{query}+{body ?? string.Empty}+{timeStamp}";
        using (SHA1 sha1 = SHA1.Create())
        {
            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(rawSignature));
            string hexSignature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return "$1$" + hexSignature;
        }
    }
}
