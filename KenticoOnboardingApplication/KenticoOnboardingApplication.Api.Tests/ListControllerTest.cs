using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Api.Controllers;
using NUnit.Framework;
using System.Web.Http;
using KenticoOnboardingApplication.Api.Models;
using KenticoOnboardingApplication.Api.Tests.Comparers;
using RouteParameter = System.Web.Http.RouteParameter;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class ListControllerTest
    {
        private ListController _controller;
        private static readonly Guid Guid = new Guid("d95f4249-6f37-46ab-b102-b55972306910");
        private static readonly Item Item = new Item {Text = "updated item"};

        private static readonly Item[] Items =
        {
            new Item {Text = "Learn C#"},
            new Item {Text = "Create dummy controller"},
            new Item {Text = "Connect JS and TS"}
        };

        [SetUp]
        public void SetUp()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                WebApiConfig.RouteName,
                "api/test/{id}",
                new {id = RouteParameter.Optional}
            );

            _controller = new ListController
            {
                Configuration = config,
                Request = new HttpRequestMessage()
            };
        }

        [Test]
        public async Task GetAllItems_ReturnsItemsAndOk()
        {
            var expectedValue = Items;

            var (status, value) = await GetStatusAndValue<Item[]>(controller => controller.GetAllItems());

            Assert.That(status, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(expectedValue).AsCollection.UsingItemComparer());
        }

        [Test]
        public async Task GetItem_WithGuid_ReturnsItemAndOk()
        {
            var expectedValue = Items[0];

            var (status, value) = await GetStatusAndValue<Item>(controller => controller.GetItem(Guid));

            Assert.That(status, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task PostItem_WithItem_ReturnsItemAndCreated()
        {
            var expectedValue = Items[1];

            var (status, value) = await GetStatusAndValue<Item>(controller => controller.PostItem(Item));

            Assert.That(status, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(value, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task PostItem_WithItem_ReturnsLocation()
        {
            const string expectedLocation = "/api/test/d95f4249-6f37-46ab-b102-b55972306910";

            var result = await _controller.PostItem(new Item {Text = "new item"});
            var executedResult = await result.ExecuteAsync(CancellationToken.None);
            var resultLocation = executedResult.Headers.Location.ToString();

            Assert.That(resultLocation, Is.EqualTo(expectedLocation));
        }

        [Test]
        public async Task PutItem_WithItemAndGuid_ReturnsItemAndOk()
        {
            var expectedValue = Items[0];

            var (status, value) = await GetStatusAndValue<Item>(controller => controller.PutItem(Guid, Item));

            Assert.That(status, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task DeleteItem_WithId_ReturnsNoContent()
        {
            var executedResult = await GetExectuedResult(controller => controller.DeleteItem(Guid));
            var resultStatus = executedResult.StatusCode;

            Assert.That(resultStatus, Is.EqualTo(HttpStatusCode.NoContent));
        }

        private async Task<(HttpStatusCode status, T value)> GetStatusAndValue<T>(
            Func<ListController, Task<IHttpActionResult>> action)
        {
            var executedResult = await GetExectuedResult(action);
            executedResult.TryGetContentValue(out T value);
            var status = executedResult.StatusCode;

            return (status, value);
        }

        private async Task<HttpResponseMessage> GetExectuedResult(Func<ListController, Task<IHttpActionResult>> action)
        {
            var result = await action(_controller);

            return await result.ExecuteAsync(CancellationToken.None);
        }
    }
}