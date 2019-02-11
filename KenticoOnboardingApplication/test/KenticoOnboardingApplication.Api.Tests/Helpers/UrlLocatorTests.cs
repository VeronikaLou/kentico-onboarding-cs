using System;
using System.Web.Http.Routing;
using System.Web.Routing;
using KenticoOnboardingApplication.Api.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Api.Tests.Helpers
{
    [TestFixture]
    public class UrlLocatorTests
    {
        [Test]
        public void GetListItemUri_WithGuid_ReturnsUriWithGuid()
        {
            var id = new Guid("00000000-0000-0000-0000-000000000001");
            var expectedUri = new Uri($"http://location/api/List/{id}/test");
            var locator = CreateLocator(id, expectedUri);

            var resultUri = locator.GetListItemUri(id);

            Assert.That(expectedUri, Is.EqualTo(resultUri));
        }

        private static UrlLocator CreateLocator(Guid id, Uri uri)
        {
            var urlHelper = Substitute.For<UrlHelper>();
            urlHelper.Link("Get", Arg.Is<object>(item => CheckItemFormat(item, id))).Returns(uri.ToString());

            return new UrlLocator(urlHelper);
        }

        private static bool CheckItemFormat(object item, Guid id)
        {
            var itemId = new RouteValueDictionary(item)["id"];
            return itemId is Guid && itemId.Equals(id);
        }
    }
}