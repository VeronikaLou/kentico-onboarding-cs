using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using Unity;
using Unity.Exceptions;
using Unity.Resolution;

namespace KenticoOnboardingApplication.Api.Resolvers
{
    internal sealed class UnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        private static readonly List<string> s_caughtExceptions = new List<string>()
        {
            nameof(IHostBufferPolicySelector),
            nameof(IHttpControllerSelector),
            nameof(IHttpControllerActivator),
            nameof(IHttpActionSelector),
            nameof(IHttpActionInvoker),
            nameof(IContentNegotiator),
            nameof(IExceptionHandler)
        };

        public UnityResolver(IUnityContainer container) =>
            _container = container ?? throw new ArgumentNullException(nameof(container));

        public object GetService(Type serviceType) =>
            ResolveServiceType(() => _container.Resolve(serviceType), null);

        public IEnumerable<object> GetServices(Type serviceType) =>
            ResolveServiceType(() => _container.ResolveAll(serviceType), Enumerable.Empty<object>());

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose() => _container.Dispose();

        private static T ResolveServiceType<T>(Func<T> resolve, T returnValue)
        {
            try
            {
                return resolve();
            }
            catch (ResolutionFailedException ex) when (s_caughtExceptions.Contains(ex.TypeRequested))
            {
                return returnValue;
            }
        }
    }
}