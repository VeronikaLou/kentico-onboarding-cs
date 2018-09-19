using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Api.Controllers;
using NUnit.Framework;
using System.Web.Http;
using KenticoOnboardingApplication.Api.Tests.Comparers;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Models;
using NSubstitute;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class ListControllerTest
    {
        private ListController _controller;
        private readonly IListRepository _repository = Substitute.For<IListRepository>();

        private static readonly Item[] Items =
        {
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000001"), Text = "Learn C#"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000002"), Text = "Create dummy controller"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000003"), Text = "Connect JS and TS"}
        };

        [SetUp]
        public void SetUp()
        {
            _controller = new ListController(_repository)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
        }

        [Test]
        public async Task GetAllItems_ReturnsItemsAndOk()
        {
            _repository.GetAllItems().Returns(Task.FromResult(Items));
            var expectedValue = Items;

            var (executedResult, value) =
                await GetExecutedResultAndValue<Item[]>(controller => controller.GetAllItems());

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(expectedValue).AsCollection.UsingItemComparer());
        }

        [Test]
        public async Task GetItem_WithGuid_ReturnsItemAndOk()
        {
            _repository.GetItem(Items[0].Id).Returns(Task.FromResult(Items[0]));
            var expectedValue = Items[0];

            var (executedResult, value) =
                await GetExecutedResultAndValue<Item>(controller => controller.GetItem(Items[0].Id));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task PostItem_WithItem_ReturnsItemAndLocationAndCreated()
        {
            _repository.AddItem(Items[1]).Returns(Task.FromResult(Items[1]));

            _controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/test")
            };
            _controller.Configuration.Routes.MapHttpRoute(
                name: "Get",
                routeTemplate: "api/{id}/test",
                defaults: new {id = RouteParameter.Optional});
            var expectedValue = Items[1];
            var expectedLocation = $"http://localhost/api/{Items[1].Id}/test";

            var (executedResult, value) =
                await GetExecutedResultAndValue<Item>(controller => controller.PostItem(Items[1]));
            var resultLocation = executedResult.Headers.Location.ToString();

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(value, Is.EqualTo(expectedValue).UsingItemComparer());
            Assert.That(resultLocation, Is.EqualTo(expectedLocation));
        }

        [Test]
        public async Task PutItem_WithItemAndGuid_ReturnsItemAndOk()
        {
            _repository.UpdateItem(Items[0].Id, Items[0]).Returns(Task.FromResult(Items[0]));
            var expectedValue = Items[0];

            var (executedResult, value) =
                await GetExecutedResultAndValue<Item>(controller => controller.PutItem(Items[0].Id, Items[0]));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task DeleteItem_WithId_ReturnsNoContent()
        {
            _repository.DeleteItem(Items[0].Id).Returns(Task.CompletedTask);
            var executedResult = await GetExectuedResult(controller => controller.DeleteItem(Items[0].Id));
            var resultStatus = executedResult.StatusCode;

            Assert.That(resultStatus, Is.EqualTo(HttpStatusCode.NoContent));
        }

        private async Task<(HttpResponseMessage executedResult, T value)> GetExecutedResultAndValue<T>(
            Func<ListController, Task<IHttpActionResult>> action)
        {
            var executedResult = await GetExectuedResult(action);
            executedResult.TryGetContentValue(out T value);

            return (executedResult, value);
        }

        private async Task<HttpResponseMessage> GetExectuedResult(Func<ListController, Task<IHttpActionResult>> action)
        {
            var result = await action(_controller);

            return await result.ExecuteAsync(CancellationToken.None);
        }
    }
}