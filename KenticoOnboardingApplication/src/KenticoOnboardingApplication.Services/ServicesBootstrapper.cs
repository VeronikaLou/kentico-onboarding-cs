using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Services;
using KenticoOnboardingApplication.Services.Helpers;
using KenticoOnboardingApplication.Services.Services;
using Unity;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.Services
{
    public class ServicesBootstrapper: IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container
                .RegisterType<IItemCreatorService, ItemCreatorService>(new HierarchicalLifetimeManager())
                .RegisterType<IItemUpdaterService, ItemUpdaterService>(new HierarchicalLifetimeManager())
                .RegisterType<IItemGetterService, ItemGetterService>(new HierarchicalLifetimeManager())
                .RegisterType<IItemRemoverService, ItemRemoverService>(new HierarchicalLifetimeManager())
                .RegisterType<IGuidGenerator, GuidGenerator>(new HierarchicalLifetimeManager())
                .RegisterType<ITimeManager, TimeManager>(new HierarchicalLifetimeManager());
    }
}
