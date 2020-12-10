using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Bogus;
using Newtonsoft.Json;
using ProjectManagementAndReporting.Models;
using Swashbuckle.Swagger.XmlComments;

namespace ProjectManagementAndReporting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
