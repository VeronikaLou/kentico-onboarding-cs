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
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using NSubstitute;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class ListControllerTests
    {
        private ListController _controller;
        private IListRepository _repository;
        private IUrlLocator _urlLocator;

        private static readonly Item[] s_items =
        {
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000001"), Text = "Learn C#"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000002"), Text = "Create dummy controller"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000003"), Text = "Connect JS and TS"}
        };

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _urlLocator = Substitute.For<IUrlLocator>();
            _controller = new ListController(_repository, _urlLocator)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
        }

        [Test]
        public async Task GetAllItems_ReturnsItemsAndOk()
        {
            _repository.GetAllItemsAsync().Returns(Task.FromResult<IEnumerable<Item>>(s_items));
            var expectedItems = s_items;

            var (executedResult, items) =
                await GetExecutedResultAndValue<IEnumerable<Item>>(controller => controller.GetAllItemsAsync());

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(items, Is.EqualTo(expectedItems).AsCollection.UsingItemComparer());
        }

        [Test]
        public async Task GetItem_WithGuid_ReturnsItemAndOk()
        {
            _repository.GetItemAsync(s_items[0].Id).Returns(Task.FromResult(s_items[0]));
            var expectedValue = s_items[0];

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller => controller.GetItemAsync(s_items[0].Id));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task PostItem_WithItem_ReturnsItemAndLocationAndCreated()
        {
            var expectedLocation = $"http://localhost/api/{s_items[1].Id}/test";
            var expectedValue = s_items[1];
            _repository.AddItemAsync(s_items[1]).Returns(Task.FromResult(s_items[1]));
            _urlLocator.GetListItemUri(s_items[1].Id).Returns(new Uri(expectedLocation));

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller => controller.PostItemAsync(s_items[1]));
            var resultLocation = executedResult.Headers.Location.ToString();

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(item, Is.EqualTo(expectedValue).UsingItemComparer());
            Assert.That(resultLocation, Is.EqualTo(expectedLocation));
        }

        [Test]
        public async Task PutItem_WithItemAndGuid_ReturnsItemAndOk()
        {
            _repository.UpdateItemAsync(s_items[0]).Returns(Task.FromResult(s_items[0]));
            var expectedValue = s_items[0];

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller => controller.PutItemAsync(s_items[0].Id, s_items[0]));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task DeleteItem_WithId_ReturnsNoContent()
        {
            var executedResult = await GetExectuedResult(controller => controller.DeleteItemAsync(s_items[0].Id));
            var resultStatus = executedResult.StatusCode;

            await _repository.Received().DeleteItemAsync(s_items[0].Id);
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