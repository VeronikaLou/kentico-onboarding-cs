using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Repositories;
using Unity;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.ListRepository
{
    public class RepositoriesBootstraper : IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container.RegisterType<IListRepository, ListRepository>(new HierarchicalLifetimeManager());
    }
}
