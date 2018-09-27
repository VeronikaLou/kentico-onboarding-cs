using System.Web.Http;

namespace KenticoOnboardingApplication.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(RouteConfig.Register);
            GlobalConfiguration.Configure(JsonConfig.Register);
            GlobalConfiguration.Configure(DependencyConfig.Register);
        }
    }
}