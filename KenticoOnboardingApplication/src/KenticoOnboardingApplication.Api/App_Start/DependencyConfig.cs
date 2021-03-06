﻿using System.Web.Http;
using KenticoOnboardingApplication.Api.Resolvers;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Repositories;
using KenticoOnboardingApplication.Services;
using Unity;

namespace KenticoOnboardingApplication.Api
{
    internal static class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            RegisterDependencies(container);
            config.DependencyResolver = new UnityResolver(container);
        }

        internal static void RegisterDependencies(IUnityContainer container) =>
            container
                .Register<ApiBootstrapper>()
                .Register<RepositoriesBootstrapper>()
                .Register<ServicesBootstrapper>();

        private static IUnityContainer Register<TBootstrapper>(this IUnityContainer container)
            where TBootstrapper : IBootstrapper, new() =>
            new TBootstrapper().Register(container);
    }
}