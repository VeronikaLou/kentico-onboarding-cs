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
            var urlHelper = Substitute.For<UrlHelper>();
            var expectedUrl = $"http://location/api/v1/List/{id}";
            var expectedUri = new Uri($"http://location/api/v1/List/{id}");
            urlHelper.Link("Get", Arg.Is<object>(item => CheckItemFormat(item, id))).Returns(expectedUrl);

            var resultUri = new UrlLocator(urlHelper).GetUri(id);

            Assert.That(expectedUri, Is.EqualTo(resultUri));
        }

        private static bool CheckItemFormat(object item, Guid id)
        {
            var itemId = new RouteValueDictionary(item)["id"];
            return itemId is Guid && itemId.Equals(id);
        }
    }
}