using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using USite.Application.Common.Interfaces;
using USite.Infrastructure.Persistence;
using USite.Infrastructure.Settings;

namespace IntegrationTests;
internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseConfiguration(new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .AddEnvironmentVariables()
            .Build());

        builder.ConfigureServices((webHostBuilder, services) =>
        {
            services.PrepareTest();

            services
                .Remove<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>((sp, options) =>
                    options.UseSqlServer(webHostBuilder.Configuration.GetDefaultConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services
                .Remove<DbContextOptions<IdentityDbContext>>()
                .AddDbContext<IdentityDbContext>((sp, options) =>
                    options.UseSqlServer(webHostBuilder.Configuration.GetDefaultConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)));

            services
                .AddDbContext<CompleteDbContext>((sp, options) =>
                    options.UseSqlServer(webHostBuilder.Configuration.GetDefaultConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(CompleteDbContext).Assembly.FullName)));

            services.Remove<ICurrentUserService>()
                .AddSingleton<ICurrentUserService>(new UserService());

            services.BuildServiceProvider()
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<ApplicationDbContextInitialiser>()
                .Initialize();
        });
    }
}