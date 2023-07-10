using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using USite.Application.Common.Interfaces;
using USite.Infrastructure.AzureDevops;
using USite.Infrastructure.AzureFileStorage;
using USite.Infrastructure.Email;
using USite.Infrastructure.Hubs;
using USite.Infrastructure.Identity;
using USite.Infrastructure.Ovh;
using USite.Infrastructure.Persistence;
using USite.Infrastructure.Persistence.Interceptors;
using USite.Infrastructure.Settings;
using USite.Infrastructure.USiteTemplating;
using Client = Duende.IdentityServer.Models.Client;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace USite.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetDefaultConnectionString(),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)),
                    ServiceLifetime.Transient
                );

        services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetDefaultConnectionString(),
                builder => builder.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)),
                    ServiceLifetime.Transient
                );

        services.AddDbContext<CompleteDbContext>(options =>
           options.UseSqlServer(configuration.GetDefaultConnectionString(),
               builder => builder.MigrationsAssembly(typeof(CompleteDbContext).Assembly.FullName)),
                   ServiceLifetime.Transient
               );


        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetDefaultConnectionString(),
                builder =>
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddSingleton<AzureDevopsConnectionHelper>();
        services.AddScoped<IAzureDevopsRepositoryHelper, AzureDevopsRepositoryHelper>();
        services.AddScoped<IAzureDevopsPipelineHelper, AzureDevopsPipelineHelper>();
        services.AddScoped<IAzureFileStorageHelper, AzureFileStorageHelper>();

        services.AddSingleton<OvhClient>();
        services.AddScoped<IOvhDomainNameHelper, OvhDomainNameHelper>();
        services.AddSingleton<USiteTemplatingHelper>();
        services.AddSingleton<IHubDeploymentContext, HubDeploymentContextWrapper>();

        services
            .AddDefaultIdentity<ApplicationUser>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // User settings.
                options.User.RequireUniqueEmail = true;

                // Sign in settings.
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, IdentityDbContext>(options =>
            {
                CreateClient(options, configuration, isDevelopment);
            })
            .AddRedirectUriValidator<CustomRedirectUriValidator>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration.GetIdentityServerAuthority();
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

            })
            .AddIdentityServerJwt()
            .AddGoogle(options =>
            {
                options.ClientId = configuration.GetIdentityServerGoogleClientId();
                options.ClientSecret = configuration.GetIdentityServerGoogleClientSecret();
            });

        return services;
    }

    private static void CreateClient(ApiAuthorizationOptions options, IConfiguration configuration, bool isDevelopment)
    {
        if (isDevelopment)
            options.Clients.Add(new Client
            {
                ClientId = "local-dev",
                ClientName = "Local Development Client",
                ClientSecrets = { new("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7149/swagger/oauth2-redirect.html", "http://localhost:7148/swagger/oauth2-redirect.html", "https://oauth.pstmn.io/v1/callback", "https://localhost:3000/callback", "http://localhost:3000/callback", "https://localhost:3000", "http://localhost:3000", "http://localhost:4200/callback" },
                AllowedCorsOrigins = { "https://localhost:7149", "http://localhost:7148", "https://localhost:3000", "http://localhost:3000", "http://localhost:4200" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "USite.PresentationAPI"
                },
                AllowOfflineAccess = true,
                RequirePkce = true
            });
        else
            options.Clients.Add(new Client
            {
                ClientName = configuration.GetIdentityServerClientName(),
                ClientId = configuration.GetIdentityServerClientId(),
                ClientSecrets = { new(configuration.GetIdentityServerClientSecret().Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = configuration.GetIdentityServerRedirectUris(),
                AllowedCorsOrigins = configuration.GetIdentityServerAllowedCorsOrigins(),
                AllowedScopes = configuration.GetIdentityServerAllowedScopes(),
                AllowOfflineAccess = configuration.GetIdentityServerAllowOfflineAccess(),
                RequirePkce = true
            });
    }

}