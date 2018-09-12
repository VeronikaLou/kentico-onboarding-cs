using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Api.Controllers;
using NUnit.Framework;
using System.Web.Http;
using RouteParameter = System.Web.Http.RouteParameter;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class ListControllerTest
    {
        private ListController _controller;

        [SetUp]
        public void SetUp()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                WebApiConfig.RouteName,
                "api/test/{id}",
                new {id = RouteParameter.Optional}
            );

            _controller = new ListController()
            {
                Configuration = config,
                Request = new HttpRequestMessage()
            };
        }

        private async Task<(HttpStatusCode status, T value)> getStatusAndValue<T>(IHttpActionResult result)
        {
            var executedResult = await result.ExecuteAsync(CancellationToken.None);
            executedResult.TryGetContentValue(out T value);
            var status = executedResult.StatusCode;
            return (status, value);
        } 

        [Test]
        public async Task GetAllItems()
        {
            var expectedValue = _controller.Items;
            var expectedStatus = HttpStatusCode.OK;

            var result = await _controller.GetAllItems();
            (var status, var value) = await getStatusAndValue<string[]>(result);
            
            Assert.AreEqual(status, expectedStatus);
            Assert.AreEqual(value, expectedValue);
        }

        [Test]
        public async Task GetItemById()
        {
            var expectedValue = _controller.Items[0];
            var expectedStatus = HttpStatusCode.OK;

            var result = await _controller.GetItem(new Guid("d95f4249-6f37-46ab-b102-b55972306910"));
            (var status, var value) = await getStatusAndValue<string>(result);

            Assert.AreEqual(status, expectedStatus);
            Assert.AreEqual(value, expectedValue);
        }

        [Test]
        public async Task PostItemStatusValue()
        {
            var expectedValue = _controller.Items[1];
            var expectedStatus = HttpStatusCode.Created;
            

            var result = await _controller.PostItem("new item");
            (var status, var value) = await getStatusAndValue<string>(result);

            Assert.AreEqual(status, expectedStatus);
            Assert.AreEqual(value, expectedValue);
        }

        [Test]
        public async Task PostItemLocation()
        {
            var expectedLocation = "/api/test/d95f4249-6f37-46ab-b102-b55972306910";

            var result = await _controller.PostItem("new item");
            var executedResult = await result.ExecuteAsync(CancellationToken.None);
            var resultLocation = executedResult.Headers.Location.ToString();

            Assert.AreEqual(resultLocation, expectedLocation);
        }

        [Test]
        public async Task PutItem()
        {
            var expectedValue = _controller.Items[0];
            var expectedStatus = HttpStatusCode.OK;

            var result = await _controller.PutItem(new Guid("d95f4249-6f37-46ab-b102-b55972306910"), "updated item");
            (var status, var value) = await getStatusAndValue<string>(result);

            Assert.AreEqual(status, expectedStatus);
            Assert.AreEqual(value, expectedValue);
        }

        [Test]
        public async Task DeleteItem()
        {
            var expectedStatus = HttpStatusCode.NoContent;

            var result = await _controller.DeleteItem(new Guid("d95f4249-6f37-46ab-b102-b55972306910"));
            var executedResult = await result.ExecuteAsync(CancellationToken.None);
            var resultStatus = executedResult.StatusCode;

            Assert.AreEqual(resultStatus, expectedStatus);
        }
    }
}
