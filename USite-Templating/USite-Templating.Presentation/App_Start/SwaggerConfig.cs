using System.Web.Http;
using WebActivatorEx;
using USite_Templating.Presentation;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace USite_Templating.Presentation
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger(c => c.SingleApiVersion("v1", "USite-Templating"))
              .EnableSwaggerUi();
        }
    }
}
