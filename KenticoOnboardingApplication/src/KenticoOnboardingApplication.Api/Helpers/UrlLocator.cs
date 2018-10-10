using System;
using System.Web.Http.Routing;
using KenticoOnboardingApplication.Contracts.Helpers;

namespace KenticoOnboardingApplication.Api.Helpers
{
    public class UrlLocator : IUrlLocator
    {
        private readonly UrlHelper _urlHelper;
        public const string RouteGet = "Get";

        public UrlLocator(UrlHelper urlHelper) => _urlHelper = urlHelper;

        public Uri GetListItemUri(Guid id)
        {
            var url = _urlHelper.Link(RouteGet, new {id});

            return new Uri(url);
        }
    }
}