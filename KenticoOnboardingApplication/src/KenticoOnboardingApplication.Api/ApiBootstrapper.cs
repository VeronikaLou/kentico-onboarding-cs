using System.Net.Http;
using System.Web;
using KenticoOnboardingApplication.Api.Helpers;
using KenticoOnboardingApplication.Contracts;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.Api
{
    public class ApiBootstrapper : IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container.RegisterType<IUrlLocator, UrlLocator>(new HierarchicalLifetimeManager())
                .RegisterType<HttpRequestMessage>(
                    new HierarchicalLifetimeManager(),
                    new InjectionFactory(
                        unityContainer => (HttpRequestMessage) HttpContext.Current.Items["MS_HttpRequestMessage"]
                    ));
    }
}