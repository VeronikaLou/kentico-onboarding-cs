using KenticoOnboardingApplication.Contracts;
using Unity;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.ListRepository
{
    public class ListRepositoryBootstrapper : IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container.RegisterType<IListRepository, ListRepository>(new HierarchicalLifetimeManager());
    }
}
