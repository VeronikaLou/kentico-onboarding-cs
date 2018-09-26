using System;
using System.Web.Http.Routing;
using System.Web.Routing;
using KenticoOnboardingApplication.Api.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class UrlLocatorTest
    {
        [Test]
        public void GetUri_ReturnsUri()
        {
            var id = new Guid("00000000-0000-0000-0000-000000000001");
            var expectedUrl = $"http://location/api/List/{id}/test";
            var expectedUri = new Uri(expectedUrl);
            var urlHelper = Substitute.For<UrlHelper>();
            urlHelper.Link("Get", Arg.Is<object>(item => CheckItemFormat(item, id))).Returns(expectedUrl);

            var resultUri = new UrlLocator(urlHelper).GetListItemUri(id);

            Assert.That(expectedUri, Is.EqualTo(resultUri));
        }

        private static bool CheckItemFormat(object item, Guid id)
        {
            var itemId = new RouteValueDictionary(item)["id"];
            return itemId is Guid && itemId.Equals(id);
        }
    }
}