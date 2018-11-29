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
    public class ServicesBootstrapper: IBootstrapper
    {
        public IUnityContainer Register(IUnityContainer container) =>
            container
                .RegisterType<ICreateItemService, CreateItemService>(new HierarchicalLifetimeManager())
                .RegisterType<IUpdateItemService, UpdateItemService>(new HierarchicalLifetimeManager())
                .RegisterType<IGetItemService, GetItemService>(new HierarchicalLifetimeManager())
                .RegisterType<IIdGenerator<Guid>, GuidGenerator>(new HierarchicalLifetimeManager())
                .RegisterType<ITimeManager, TimeManager>(new HierarchicalLifetimeManager());
    }
}
