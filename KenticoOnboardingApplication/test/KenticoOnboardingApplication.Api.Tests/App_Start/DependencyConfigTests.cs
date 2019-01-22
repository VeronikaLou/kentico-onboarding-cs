using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Helpers;
using NSubstitute;
using NUnit.Framework;
using Unity;

namespace KenticoOnboardingApplication.Api.Tests.App_Start
{
    [TestFixture]
    public class DependencyConfigTests
    {
        private static readonly IEnumerable<Type> s_excludedContractsTypes = new[]
        {
            typeof(IBootstrapper),
            typeof(IIdGenerator<>)
        };

        private static readonly IEnumerable<Type> s_expectedRegistrationTypes = new[]
        {
            typeof(HttpRequestMessage),
            typeof(IIdGenerator<Guid>)
        };

        [Test]
        public void RegisterDependencies_RegistersCorrectDependencies()
        {
            var expectedRegistrationTypes = GetExpectedRegistrationTypes();
            var resultRegistrationTypes = new List<Type>();
            var container = CreateContainer(resultRegistrationTypes);

            DependencyConfig.RegisterDependencies(container);

            Assert.That(resultRegistrationTypes.Except(expectedRegistrationTypes), Is.Empty, "Some redundant types were registered.");
            Assert.That(expectedRegistrationTypes.Except(resultRegistrationTypes), Is.Empty, "Not all expected types were registered.");
        }

        private static IUnityContainer CreateContainer(ICollection<Type> resultRegistrationTypes)
        {
            var container = Substitute.For<IUnityContainer>();
            container.RegisterType(Arg.Any<Type>(), Arg.Any<Type>()).ReturnsForAnyArgs(
                callInfo =>
                {
                    var typeFrom = callInfo.ArgAt<Type>(0);
                    var typeTo = callInfo.ArgAt<Type>(1);
                    resultRegistrationTypes.Add(typeFrom ?? typeTo);
                    return container;
                });

            return container;
        }

        private static List<Type> GetExpectedRegistrationTypes()
        {
            var contractsAssembly = typeof(IBootstrapper).Assembly;
            var expectedContractsTypes = contractsAssembly
                .GetTypes()
                .Where(type => type.IsInterface && !s_excludedContractsTypes.Contains(type))
                .ToList();

            return expectedContractsTypes.Union(s_expectedRegistrationTypes).ToList();
        }
    }
}