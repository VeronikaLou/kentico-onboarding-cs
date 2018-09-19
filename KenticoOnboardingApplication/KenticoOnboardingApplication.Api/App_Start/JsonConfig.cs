using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace KenticoOnboardingApplication.Api
{
    public static class JsonConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}