using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Api.Controllers;
using NUnit.Framework;
using System.Web.Http;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;
using KenticoOnboardingApplication.Tests.Base.Comparers;
using NSubstitute;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class ListControllerTests
    {
        private ListController _controller;
        private IListRepository _repository;
        private IUrlLocator _urlLocator;
        private ICreateItemService _createItemService;
        private IUpdateItemService _updateItemService;
        private IGetItemService _getItemService;

        private static readonly Item[] s_items =
        {
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000002"), Text = "Create dummy controller"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000003"), Text = "Connect JS and TS"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000004"), Text = "Learn C#"},
        };

        private static IEnumerable<Item> PostTestInvalidItems() =>
            new List<Item>
            {
                new Item {Text = String.Empty},
                new Item(),
                new Item
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Text = "Set id."
                },
                new Item {Text = "Set CreationTime", CreationTime = new DateTime(2017, 12, 24, 13, 55, 59)},
                new Item {Text = "Set LastUpdateTime", LastUpdateTime = new DateTime(2018, 1, 15, 10, 25, 36)}
            };

        private static IEnumerable<Item> PutTestInvalidItems() =>
            new List<Item>
            {
                new Item {Text = "Connect JS and TS"},
                new Item {Id = new Guid("00000000-0000-0000-0000-000000000004"), Text = String.Empty},
                new Item {Id = new Guid("00000000-0000-0000-0000-000000000003")},
                new Item
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Text = "Set creation time.",
                    CreationTime = new DateTime(2005, 5, 5, 5, 5, 5, 5)
                },
                new Item
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    Text = "Set last update time.",
                    LastUpdateTime = new DateTime(2012, 12, 21, 1, 0, 5)
                }
            };

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _urlLocator = Substitute.For<IUrlLocator>();
            _createItemService = Substitute.For<ICreateItemService>();
            _updateItemService = Substitute.For<IUpdateItemService>();
            _getItemService = Substitute.For<IGetItemService>();

            _controller = new ListController(_repository, _urlLocator, _createItemService, _updateItemService,
                _getItemService)
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
        public async Task GetItem_WithExistingId_ReturnsItemAndOk()
        {
            _getItemService
                .GetItemAsync(s_items[0].Id)
                .Returns(new RetrievedItem<Item>(s_items[0]));
            var expectedValue = s_items[0];

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller => controller.GetItemAsync(s_items[0].Id));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task GetItem_WithEmptyId_ReturnsBadRequest()
        {
            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller =>
                    controller.GetItemAsync(Guid.Empty));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(item, Is.Null);
        }

        [Test]
        public async Task GetItem_WithNonexistingId_ReturnsNotFound()
        {
            _getItemService
                .GetItemAsync(s_items[1].Id)
                .Returns(new RetrievedItem<Item>(null));

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller => controller.GetItemAsync(s_items[1].Id));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(item, Is.Null);
        }

        [Test]
        public async Task PostItem_WithValidItem_ReturnsItemAndLocationAndCreated()
        {
            var expectedLocation = new Uri($"http://localhost/api/{s_items[1].Id}/test");
            var expectedValue = s_items[1];
            var postItem = s_items[1];
            postItem.Id = Guid.Empty;
            _createItemService
                .CreateItemAsync(postItem)
                .Returns(s_items[1]);
            _urlLocator.GetListItemUri(s_items[1].Id).Returns(expectedLocation);

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller => controller.PostItemAsync(postItem));
            var resultLocation = executedResult.Headers.Location;

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(item, Is.EqualTo(expectedValue).UsingItemComparer());
            Assert.That(resultLocation, Is.EqualTo(expectedLocation));
        }

        [Test]
        [TestCaseSource(nameof(PostTestInvalidItems))]
        public async Task PostItem_WithInvalidItem_ReturnsBadRequest(Item postItem)
        {
            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller =>
                    controller.PostItemAsync(postItem));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(item, Is.Null);
        }

        [Test]
        public async Task PutItem_WithItemFromDb_ReturnsItemAndOk()
        {
            _updateItemService
                .UpdateItemAsync(s_items[0])
                .Returns(new RetrievedItem<Item>(s_items[0]));
            var expectedValue = s_items[0];

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller =>
                    controller.PutItemAsync(s_items[0].Id, s_items[0]));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(expectedValue).UsingItemComparer());
        }

        [Test]
        public async Task PutItem_WithItemWhichIsNotInDb_ReturnsNotFound()
        {
            _updateItemService
                .UpdateItemAsync(s_items[2])
                .Returns(new RetrievedItem<Item>(null));

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller =>
                    controller.PutItemAsync(s_items[2].Id, s_items[2]));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(item, Is.Null);
        }

        [Test]
        [TestCaseSource(nameof(PutTestInvalidItems))]
        public async Task PutItem_WithInvalidItem_ReturnsBadRequest(Item putItem)
        {
            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller =>
                    controller.PutItemAsync(putItem.Id, putItem));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(item, Is.Null);
        }


        [Test]
        public async Task DeleteItem_WithExistingId_ReturnsNoContent()
        {
            _getItemService.GetItemAsync(s_items[0].Id).Returns(new RetrievedItem<Item>(s_items[0]));
            var executedResult =
                await GetExectuedResult(controller => controller.DeleteItemAsync(s_items[0].Id));
            var resultStatus = executedResult.StatusCode;

            await _repository.Received().DeleteItemAsync(s_items[0].Id);
            Assert.That(resultStatus, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task DeleteItem_WithNonexistingId_ReturnsNotFound()
        {
            _getItemService.GetItemAsync(s_items[1].Id).Returns(new RetrievedItem<Item>(null));
            var executedResult =
                await GetExectuedResult(controller => controller.DeleteItemAsync(s_items[1].Id));
            var resultStatus = executedResult.StatusCode;

            Assert.That(resultStatus, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task DeleteItem_WithEmptyId_ReturnsBadRequest()
        {
            var executedResult =
                await GetExectuedResult(controller => controller.DeleteItemAsync(Guid.Empty));
            var resultStatus = executedResult.StatusCode;

            _repository.DidNotReceive();
            Assert.That(resultStatus, Is.EqualTo(HttpStatusCode.BadRequest));
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