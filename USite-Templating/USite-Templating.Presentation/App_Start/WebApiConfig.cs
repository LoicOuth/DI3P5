using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace USite_Templating.Presentation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
             name: "Template",
             routeTemplate: "api/template/generate/{action}",
             defaults: new { controller = "Template" }
            );
        }
    }
}
