using System.Web.Http;

namespace KenticoOnboardingApplication.Api
{
    public static class WebApiConfig
    {
        internal static readonly string RouteName = "ApiV1";

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: RouteName,
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new {controller = "List", id = RouteParameter.Optional}
            );
        }
    }
}