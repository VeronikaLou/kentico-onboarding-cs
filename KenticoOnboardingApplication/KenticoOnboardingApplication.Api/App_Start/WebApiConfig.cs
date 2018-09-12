using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KenticoOnboardingApplication.Api
{
    public static class WebApiConfig
    {
        internal static readonly string RouteName = "DefaultApi";

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: RouteName,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
