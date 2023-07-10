using Microsoft.Extensions.DependencyInjection;
using Moq;
using USite.Application.Common.Interfaces;

namespace IntegrationTests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Remove<TService>(this IServiceCollection services)
    {
        var serviceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(TService));

        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
        }

        return services;
    }

    public static IServiceCollection PrepareTest(this IServiceCollection services)
    {
        var ovhHelper = new Mock<IOvhDomainNameHelper>();

        ovhHelper.Setup(x => x.CheckSubdomainAvailability("test")).ReturnsAsync(false);
        ovhHelper.Setup(x => x.CheckSubdomainAvailability("test2")).ReturnsAsync(true);
        ovhHelper.Setup(x => x.CreateSubDomain("test")).ReturnsAsync("usite.fr");

        services.Remove<IOvhDomainNameHelper>();
        services.AddSingleton(ovhHelper.Object);

        var azureFileStorageHelper = new Mock<IAzureFileStorageHelper>();

        azureFileStorageHelper.Setup(x => x.DeleteFileIfExist("existingFile")).ReturnsAsync(true);
        azureFileStorageHelper.Setup(x => x.UploadFile(null, "existingUri")).ReturnsAsync("newUri");

        services.Remove<IAzureFileStorageHelper>();
        services.AddSingleton(azureFileStorageHelper.Object);

        return services;
    }
}