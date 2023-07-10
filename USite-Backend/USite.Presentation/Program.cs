using Duende.IdentityServer;
using Microsoft.OpenApi.Models;
using USite.Application;
using USite.Infrastructure;
using USite.Infrastructure.Hubs;
using USite.Infrastructure.Persistence;
using USite.Infrastructure.Settings;
using USite.Presentation;
using USite.Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);

//add swagger to services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Name = "Bearer",
        Description = "Authorization using the OAuth2 access token authorization flow",
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:7149/connect/authorize"),
                TokenUrl = new Uri("https://localhost:7149/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {IdentityServerConstants.StandardScopes.OpenId, "OpenId"},
                    {IdentityServerConstants.StandardScopes.Profile, "Profile"},
                    {IdentityServerConstants.StandardScopes.OfflineAccess, "OfflineAccess"},
                    {"USite.PresentationAPI", "USite.PresentationAPI"},
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "OAuth2" }
            },
            new List<string>()
        }
    });
});

builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");
//add custom services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddPresentationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins", policy =>
        {
            policy.WithOrigins(builder.Configuration.GetIdentityServerAllowedCorsOrigins().ToArray())
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSignalR();
}
else
{
    builder.Services.AddSignalR().AddStackExchangeRedis(builder.Configuration.GetRedisConnectionString());
}

//build the app
var app = builder.Build();

//configure the dev environment
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initializer.SeedAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("local-dev");
        options.OAuthClientSecret("secret"); //TODO : WAIT FOR THE ISSUES TO BE FIXED https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/2470
        options.OAuthUsePkce();
    });
}
else
{
    app.UseHsts();
}

app.Use(async (ctx, next) =>
{
    ctx.Request.Scheme = "https";
    await next();
});

app.UseCors("MyAllowedOrigins");
app.UseHealthChecks("/api/health");
//use wwwroot folder 
app.UseStaticFiles();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

//use razor pages
app.UseRequestLocalization("en-US", "fr-FR", "es-ES");
app.MapRazorPages();

//Hub
app.MapHub<HubElement>("/hub");
app.MapHub<HubDeployment>("/deployment");

//use controllers
app.MapControllers();
app.Run();
