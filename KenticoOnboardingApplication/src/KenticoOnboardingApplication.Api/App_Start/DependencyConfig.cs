using System.Web.Http;
using KenticoOnboardingApplication.Api.Utils;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.ListRepository;
using Unity;

namespace KenticoOnboardingApplication.Api
{
    internal static class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer()
                .Register<ApiBootstrapper>()
                .Register<ListRepositoryBootstrapper>();
            config.DependencyResolver = new UnityResolver(container);
        }

        private static IUnityContainer Register<T>(this IUnityContainer container) 
            where T : IBootstrapper, new() =>
            new T().Register(container);
    }
}