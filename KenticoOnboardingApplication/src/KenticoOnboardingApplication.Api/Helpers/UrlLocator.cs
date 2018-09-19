using System;
using System.Web.Http.Routing;
using KenticoOnboardingApplication.Contracts;

namespace KenticoOnboardingApplication.Api.Helpers
{
    public class UrlLocator : IUrlLocator
    {
        private readonly UrlHelper _urlHelper;

        public UrlLocator(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public Uri GetUri(Guid id)
        {
            var url = _urlHelper.Link("Get", new {id});
            var uri = new Uri(url);

            return uri;
        }
    }
}