using System.Configuration;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Repositories;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.Repositories
{
    public class RepositoriesBootstraper : IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container.RegisterType<IListRepository, Repositories.ListRepository>(
                new HierarchicalLifetimeManager(),
                InjectConnectionString()
            );

        private static InjectionConstructor InjectConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["TODOList"].ConnectionString;
            return new InjectionConstructor(connectionString);
        }
    }
}