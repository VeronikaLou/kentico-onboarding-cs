using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Api.Controllers;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;
using KenticoOnboardingApplication.Contracts.Services.Wrappers;
using KenticoOnboardingApplication.Tests.Base;
using KenticoOnboardingApplication.Tests.Base.Factories;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Api.Tests.Controllers
{
    [TestFixture]
    public class ListControllerTests
    {
        private ListController _controller;
        private IListRepository _repository;
        private IUrlLocator _urlLocator;
        private IGetItemService _getItemService;
        private ICreateItemService _createItemService;

        private static IEnumerable<(Item errorneousItem, string responseValidationKey)> PostTestInvalidItems() =>
            new List<(Item, string)>
            {
                (ItemsCreator.CreateItem(), nameof(Item.Text)),
                (ItemsCreator.CreateItem(id: "00000000-0000-0000-0000-000000000001", text: "Set id"), nameof(Item.Id)),
                (ItemsCreator.CreateItem(text: "Set CreationTime", creationTime: "2017-12-24"),
                    nameof(Item.CreationTime)),
                (ItemsCreator.CreateItem(text: "Set LastUpdateTime", lastUpdateTime: "2018-05-01"),
                    nameof(Item.LastUpdateTime)),
            };

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _urlLocator = Substitute.For<IUrlLocator>();
            _getItemService = Substitute.For<IGetItemService>();
            _createItemService = Substitute.For<ICreateItemService>();

            _controller = new ListController(_repository, _urlLocator, _getItemService, _createItemService)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
        }

        [Test]
        public async Task GetAllItems_ReturnsItemsAndOk()
        {
            var expectedItems = new[]
            {
                ItemsCreator.CreateItem(id: "00000000-0000-0000-0000-000000000001", text: "Learn C#"),
                ItemsCreator.CreateItem(id: "00000000-0000-0000-0000-000000000002", text: "Create dummy controller"),
                ItemsCreator.CreateItem(id: "00000000-0000-0000-0000-000000000003", text: "Connect JS and TS")
            };

            _repository.GetAllItemsAsync().Returns(Task.FromResult<IEnumerable<Item>>(expectedItems));

            var (executedResult, items) =
                await GetExecutedResultAndValue<IEnumerable<Item>>(controller => controller.GetAllItemsAsync());

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(items, Is.EqualTo(expectedItems).AsCollection);
        }

        [Test]
        public async Task GetItem_WithExistingId_ReturnsItemAndOk()
        {
            var itemAndRetrievedItem = ItemsCreator.CreateItemAndRetrievedItem(id: "00000000-0000-0000-0000-000000000001", text: "Learn C#");
            _getItemService
                .GetItemAsync(itemAndRetrievedItem.item.Id)
                .Returns(itemAndRetrievedItem.retrievedItem);

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller =>
                    controller.GetItemAsync(itemAndRetrievedItem.item.Id));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(itemAndRetrievedItem.item));
        }

        [Test]
        public async Task GetItem_WithEmptyId_ReturnsBadRequest()
        {
            var (executedResult, error) =
                await GetExecutedResultAndValue<HttpError>(controller =>
                    controller.GetItemAsync(Guid.Empty));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(error.ModelState, Does.ContainKey(nameof(Item.Id)));
            Assert.That(error.ModelState, Has.One.Items);
        }

        [Test]
        public async Task GetItem_WithNotExistingId_ReturnsNotFound()
        {
            var id = new Guid("00000000-0000-0000-0000-000000000003");
            _getItemService
                .GetItemAsync(id)
                .Returns(RetrievedItem<Item>.Empty);

            var executedResult = await GetExecutedResult(controller => controller.GetItemAsync(id));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task PostItem_WithValidItem_ReturnsItemAndLocationAndCreated()
        {
            var expectedItem = ItemsCreator.CreateItem("00000000-0000-0000-0000-000000000002", "Create dummy controller");
            var postItem = ItemsCreator.CreateItem(text: expectedItem.Text);
            var expectedLocation = new Uri($"http://localhost/api/{expectedItem.Id}/test");
            ArrangeUrlLocatorAndCreateItemService(expectedItem, postItem, expectedLocation);

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller => controller.PostItemAsync(postItem));
            var resultLocation = executedResult.Headers.Location;

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(item, Is.EqualTo(expectedItem));
            Assert.That(resultLocation, Is.EqualTo(expectedLocation));
        }

        [Test]
        [TestCaseSource(nameof(PostTestInvalidItems))]
        public async Task PostItem_WithInvalidItem_ReturnsBadRequest((Item item, string responseValidationKey) itemWithError)
        {
            var (executedResult, error) =
                await GetExecutedResultAndValue<HttpError>(controller =>
                    controller.PostItemAsync(itemWithError.item));

            Assert.That(error.ModelState, Has.One.Items);
            Assert.That(error.ModelState, Does.ContainKey(itemWithError.responseValidationKey));
            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task PutItem_WithItemAndGuid_ReturnsItemAndOk()
        {
            var itemToPut = ItemsCreator.CreateItem("00000000-0000-0000-0000-000000000001", "Learn C#");
            _repository.UpdateItemAsync(itemToPut).Returns(Task.FromResult(itemToPut));

            var (executedResult, item) =
                await GetExecutedResultAndValue<Item>(controller => controller.PutItemAsync(itemToPut.Id, itemToPut));

            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(itemToPut).UsingItemComparer());
        }

        [Test]
        public async Task DeleteItem_WithId_ReturnsNoContent()
        {
            var itemToDelete = ItemsCreator.CreateItem("00000000-0000-0000-0000-000000000001", "Learn C#");
            var executedResult = await GetExecutedResult(controller => controller.DeleteItemAsync(itemToDelete.Id));
            var resultStatus = executedResult.StatusCode;

            await _repository.Received().DeleteItemAsync(itemToDelete.Id);
            Assert.That(resultStatus, Is.EqualTo(HttpStatusCode.NoContent));
        }

        private async Task<(HttpResponseMessage executedResult, T value)> GetExecutedResultAndValue<T>(
            Func<ListController, Task<IHttpActionResult>> action)
        {
            var executedResult = await GetExecutedResult(action);
            executedResult.TryGetContentValue(out T value);

            return (executedResult, value);
        }

        private async Task<HttpResponseMessage> GetExecutedResult(Func<ListController, Task<IHttpActionResult>> action)
        {
            var result = await action(_controller);

            return await result.ExecuteAsync(CancellationToken.None);
        }

        private void ArrangeUrlLocatorAndCreateItemService(Item item, Item postItem, Uri expectedLocation)
        {
            _urlLocator.GetListItemUri(item.Id).Returns(expectedLocation);
            _createItemService
                .CreateItemAsync(ArgWrapper.IsItem(postItem))
                .Returns(item);
        }
    }
}