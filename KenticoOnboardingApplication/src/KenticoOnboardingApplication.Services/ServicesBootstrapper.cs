using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Services;
using KenticoOnboardingApplication.Services.Services;
using Unity;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.Services
{
    public class ServicesBootstrapper : IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container
                .RegisterType<IGetItemService, GetItemService>(new HierarchicalLifetimeManager());
    }
}