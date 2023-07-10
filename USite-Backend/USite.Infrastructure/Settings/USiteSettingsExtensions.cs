using Microsoft.Extensions.Configuration;

namespace USite.Infrastructure.Settings;

public static class USiteSettingsExtensions
{
    public static string GetDefaultConnectionString(this IConfiguration configuration) =>
        configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection is not configured");

    public static string GetRedisConnectionString(this IConfiguration configuration) => 
        configuration.GetConnectionString("RedisConnection") ?? throw new InvalidOperationException("RedisConnection is not configured");

    public static string GetPresentationLink(this IConfiguration configuration) =>
    configuration.GetValue<string>("PresentationLink") ?? throw new InvalidOperationException("PresentationLink is not configured");

    public static string GetCmsLink(this IConfiguration configuration) =>
        configuration.GetValue<string>("CmsLink") ?? throw new InvalidOperationException("CmsLink is not configured");

    public static string GetIdentityServerClientName(this IConfiguration configuration) =>
        configuration.GetSection("Authentication:IdentityServer")["ClientName"] ?? throw new InvalidOperationException("IdentityServer ClientName is not configured");

    public static string GetIdentityServerClientId(this IConfiguration configuration) =>
        configuration.GetSection("Authentication:IdentityServer")["ClientId"] ?? throw new InvalidOperationException("IdentityServer ClientId is not configured");

    public static string GetIdentityServerClientSecret(this IConfiguration configuration) =>
        configuration.GetSection("Authentication:IdentityServer")["ClientSecret"] ?? throw new InvalidOperationException("IdentityServer ClientSecret is not configured");

    public static List<string> GetIdentityServerRedirectUris(this IConfiguration configuration) =>
        configuration.GetSection("Authentication:IdentityServer").GetSection("RedirectUris").Get<List<string>>() ?? throw new InvalidOperationException("IdentityServer RedirectUris is not configured");

    public static List<string> GetIdentityServerAllowedCorsOrigins(this IConfiguration configuration) =>
        configuration.GetSection("Authentication:IdentityServer").GetSection("AllowedCorsOrigins").Get<List<string>>() ?? throw new InvalidOperationException("IdentityServer AllowedCorsOrigins is not configured");

    public static List<string> GetIdentityServerAllowedScopes(this IConfiguration configuration) =>
        configuration.GetSection("Authentication:IdentityServer").GetSection("AllowedScopes").Get<List<string>>() ?? throw new InvalidOperationException("IdentityServer AllowedScopes is not configured");

    public static bool GetIdentityServerAllowOfflineAccess(this IConfiguration configuration) =>
       configuration.GetSection("Authentication:IdentityServer").GetValue<bool>("AllowOfflineAccess");

    public static string GetIdentityServerAuthority(this IConfiguration configuration) =>
        configuration.GetSection("Authentication:IdentityServer")["Authority"] ?? throw new InvalidOperationException("IdentityServer Authority is not configured");

    public static string GetIdentityServerGoogleClientId(this IConfiguration configuration) =>
        configuration.GetSection("Authentication:Google")["ClientId"] ?? throw new InvalidOperationException("Google ClientId is not configured");

    public static string GetIdentityServerGoogleClientSecret(this IConfiguration configuration) =>
        configuration.GetSection("Authentication:Google")["ClientSecret"] ?? throw new InvalidOperationException("Google ClientSecret is not configured");

    //Azure devops
    public static string GetAzureDevopsPAT(this IConfiguration configuration) =>
          configuration.GetSection("AzureDevops")["PAT"] ?? throw new InvalidOperationException("AzureDevops PAT is not configured");

    //Azure storage
    public static string GetAzureStorageConnection(this IConfiguration configuration) =>
          configuration.GetSection("AzureStorage")["ConnectionString"] ?? throw new InvalidOperationException("AzureStorage connection is not configured");
    public static string GetAzureStorageContainerName(this IConfiguration configuration) =>
          configuration.GetSection("AzureStorage")["ContainerName"] ?? throw new InvalidOperationException("AzureStorage ContainerName is not configured");
    
    public static string GetDefaultImageUrl(this IConfiguration configuration) =>
        configuration.GetSection("AzureStorage")["DefaultImageUrl"] ?? throw new InvalidOperationException("AzureStorage DefaultImageUrl is not configured");
    //Ovh
    public static string GetOvhApiKey(this IConfiguration configuration) =>
        configuration.GetSection("Ovh")["ApiKey"] ?? throw new InvalidOperationException("Ovh api key is not configured");

    public static string GetOvhApiSecret(this IConfiguration configuration) =>
        configuration.GetSection("Ovh")["ApiSecret"] ?? throw new InvalidOperationException("Ovh api secret is not configured");

    public static string GetOvhConsumerKey(this IConfiguration configuration) =>
        configuration.GetSection("Ovh")["ConsumerKey"] ?? throw new InvalidOperationException("Ovh consumer key is not configured");

    public static string GetOvhTarget(this IConfiguration configuration) =>
        configuration.GetSection("Ovh")["Target"] ?? throw new InvalidOperationException("Ovh Target is not configured");

    //USite templating
    public static string GetUSiteTemplatingUrl(this IConfiguration configuration) =>
     configuration.GetSection("USiteTemplating")["Url"] ?? throw new InvalidOperationException("USite templating Url is not configured");

    //Email
    public static string GetOvhEmailPassword(this IConfiguration configuration) =>
        configuration.GetSection("EmailSender")["Password"] ?? throw new InvalidOperationException("Email Password is not configured");

}
