using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Api.Controllers;
using NUnit.Framework;
using System.Web.Http;
using KenticoOnboardingApplication.Api.Tests.Comparers;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using NSubstitute;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class ListControllerTest
    {
        private ListController _controller;
        private readonly IListRepository _repository = Substitute.For<IListRepository>();
        private readonly IUrlLocator _urlLocator = Substitute.For<IUrlLocator>();

        private static readonly Item[] Items =
        {
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000001"), Text = "Learn C#"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000002"), Text = "Create dummy controller"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000003"), Text = "Connect JS and TS"}
        };

        [SetUp]
        public void SetUp()
        {
            _controller = new ListController(_repository, _urlLocator)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
        }

        [Test]
        public async Task GetAllItems_ReturnsItemsAndOk()
        {
            _repository.GetAllItemsAsync().Returns(Task.FromResult<IEnumerable<Item>>(Items));
            var expectedValue = Items;

            var (executedResult, value) =
                await GetExecutedResultAndValue<Item[]>(controller => controller.GetAllItemsAsync());

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(expectedValue).AsCollection.UsingItemComparer());
        }

        [Test]
        public async Task GetItem_WithGuid_ReturnsItemAndOk()
        {
            _repository.GetItemAsync(Arg.Any<Guid>()).Returns(Task.FromResult(Items[0]));
            var expectedValue = Items[0];

            var (executedResult, value) =
                await GetExecutedResultAndValue<Item>(controller => controller.GetItemAsync(Items[0].Id));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task PostItem_WithItem_ReturnsItemAndLocationAndCreated()
        {
            var expectedLocation = $"http://localhost/api/{Items[1].Id}/test";
            var expectedValue = Items[1];
            _repository.AddItemAsync(Arg.Any<Item>()).Returns(Task.FromResult(Items[1]));
            _urlLocator.GetListItemUri(Arg.Any<Guid>()).Returns(new Uri(expectedLocation));


            var (executedResult, value) =
                await GetExecutedResultAndValue<Item>(controller => controller.PostItemAsync(Items[1]));
            var resultLocation = executedResult.Headers.Location.ToString();

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(value, Is.EqualTo(expectedValue).UsingItemComparer());
            Assert.That(resultLocation, Is.EqualTo(expectedLocation));
        }

        [Test]
        public async Task PutItem_WithItemAndGuid_ReturnsItemAndOk()
        {
            _repository.UpdateItemAsync(Arg.Any<Item>()).Returns(Task.FromResult(Items[0]));
            var expectedValue = Items[0];

            var (executedResult, value) =
                await GetExecutedResultAndValue<Item>(controller => controller.PutItemAsync(Items[0].Id, Items[0]));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task DeleteItem_WithId_ReturnsNoContent()
        {
            _repository.DeleteItemAsync(Arg.Any<Guid>()).Returns(Task.CompletedTask);
            var executedResult = await GetExectuedResult(controller => controller.DeleteItemAsync(Items[0].Id));
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