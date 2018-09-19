using System.Web.Http;
using KenticoOnboardingApplication.Api.Utils;
using KenticoOnboardingApplication.Contracts.Interfaces;
using Unity;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.Api
{
    public static class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IListRepository, ListRepository.ListRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}