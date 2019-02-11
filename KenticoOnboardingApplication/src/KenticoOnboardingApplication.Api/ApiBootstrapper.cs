using System.Net.Http;
using System.Web;
using KenticoOnboardingApplication.Api.Helpers;
using KenticoOnboardingApplication.Api.Repositories;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Repositories;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.Api
{
    public class ApiBootstrapper : IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container
                .RegisterType<IUrlLocator, UrlLocator>(new HierarchicalLifetimeManager())
                .RegisterType<HttpRequestMessage>(
                    new HierarchicalLifetimeManager(),
                    InjectHttpMessage()
                )
                .RegisterType<IConnectionStrings, ConnectionStrings>(new HierarchicalLifetimeManager());

        private static InjectionFactory InjectHttpMessage() =>
            new InjectionFactory(
                unityContainer => (HttpRequestMessage) HttpContext.Current.Items["MS_HttpRequestMessage"]
            );
    }
}