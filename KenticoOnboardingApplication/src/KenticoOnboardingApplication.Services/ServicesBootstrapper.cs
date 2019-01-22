using System;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Services;
using KenticoOnboardingApplication.Services.Helpers;
using KenticoOnboardingApplication.Services.Services;
using Unity;
using Unity.Lifetime;

namespace KenticoOnboardingApplication.Services
{
    public class ServicesBootstrapper : IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container
                .RegisterType<IGetItemService, GetItemService>(new HierarchicalLifetimeManager())
                .RegisterType<ICreateItemService, CreateItemService>(new HierarchicalLifetimeManager())
                .RegisterType<ITimeManager, TimeManager>(new HierarchicalLifetimeManager())
                .RegisterType<IIdGenerator<Guid>, GuidGenerator>(new HierarchicalLifetimeManager());
    }
}