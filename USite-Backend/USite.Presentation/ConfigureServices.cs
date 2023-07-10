using USite.Application.Common.Interfaces;
using USite.Infrastructure.Persistence;
using USite.Presentation.Filters;
using USite.Presentation.Services;


namespace USite.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
        

        services.AddHsts(options =>
        {
            //TODO : Configure Hsts according to OWASP cheatsheet : https://cheatsheetseries.owasp.org/cheatsheets/DotNet_Security_Cheat_Sheet.html
        });

        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
        });

        return services;
    }
}