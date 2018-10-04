using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using KenticoOnboardingApplication.Contracts;
using NUnit.Framework;
using Unity;
using Unity.Extension;
using Unity.Lifetime;
using Unity.Registration;
using Unity.Resolution;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class DependencyConfigTests
    {
        [Test]
        public void Register_RegistersCorrectDependencies()
        {
            // dat IBootstrapper a HttpReqMes do kolekci
            var notRegisteredTypes = new List<Type>
            {
                typeof(IBootstrapper)
            };

            var contractsAssembly = typeof(IBootstrapper).Assembly;
            var expectedRegistrations = contractsAssembly
                .GetTypes()
                .Where(type => type.IsInterface && !notRegisteredTypes.Contains(type))
                .ToList();
            expectedRegistrations.Add(typeof(HttpRequestMessage));

            foreach (var registration in expectedRegistrations)
            {
                Console.WriteLine(registration);
            }

            Console.WriteLine();


            var actualRegistrations = new List<Type>();
            ;
            var container = new MockContainer(actualRegistrations);

            DependencyConfig.RegisterDependencies(container);
            var resultRegistrations = container.RegisteredTypes
                .ToArray();

            foreach (var registration in resultRegistrations)
            {
                Console.WriteLine(registration);
            }

            Assert.That(resultRegistrations.Except(expectedRegistrations), Is.Empty);
            Assert.That(expectedRegistrations.Except(resultRegistrations), Is.Empty);
        }
    }

    internal class MockContainer : IUnityContainer
    {
        public ICollection<Type> RegisteredTypes { get; }

        public MockContainer(ICollection<Type> registeredTypes) => RegisteredTypes = registeredTypes;
        
        public IEnumerable<IContainerRegistration> Registrations => throw new NotImplementedException();

        public IUnityContainer RegisterType(Type typeFrom, Type typeTo, string name, LifetimeManager lifetimeManager,
            params InjectionMember[] injectionMembers)

        {
            RegisteredTypes.Add(typeFrom);
            return this;
        }

        public bool IsRegistered(Type type, string name) => throw new NotImplementedException();

        public IUnityContainer Parent => throw new NotImplementedException();

        public void Dispose() => throw new NotImplementedException();

        public IUnityContainer RegisterInstance(Type type, string name, object instance, LifetimeManager lifetime) =>
            throw new NotImplementedException();

        public object Resolve(Type type, string name, params ResolverOverride[] resolverOverrides) =>
            throw new NotImplementedException();

        public object BuildUp(Type type, object existing, string name, params ResolverOverride[] resolverOverrides) =>
            throw new NotImplementedException();

        public IUnityContainer AddExtension(UnityContainerExtension extension) => throw new NotImplementedException();

        public object Configure(Type configurationInterface) => throw new NotImplementedException();

        public IUnityContainer CreateChildContainer() => throw new NotImplementedException();
    }
}