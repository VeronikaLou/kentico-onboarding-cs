using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Repositories;
using Unity;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.Repositories
{
    public class RepositoriesBootstraper : IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container.RegisterType<IListRepository, Repositories.ListRepository>(new HierarchicalLifetimeManager());
    }
}
