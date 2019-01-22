using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;
using KenticoOnboardingApplication.Contracts.Services.Wrappers;
using KenticoOnboardingApplication.Services.Services;
using KenticoOnboardingApplication.Tests.Base;
using KenticoOnboardingApplication.Tests.Base.Factories;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Services.Tests.Services
{
    [TestFixture]
    public class UpdateItemTests
    {
        private IListRepository _repository;
        private UpdateItemService _updateItemService;
        private IGetItemService _getItemService;
        private ITimeManager _timeManager;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _timeManager = Substitute.For<ITimeManager>();
            _getItemService = Substitute.For<IGetItemService>();
            _updateItemService = new UpdateItemService(_repository, _timeManager, _getItemService);
        }

        [Test]
        public async Task UpdateItemAsync_WithItemFromDb_ReturnsItemAndTrue()
        {
            var (expectedItemAndRetrievedItem, itemForUpdate) = GetExpectedItemAndItemForUpdate();
            ArrangeTestAccordingToItem(expectedItemAndRetrievedItem.Item, itemForUpdate);

            var result = await _updateItemService.UpdateItemAsync(itemForUpdate);

            Assert.That(result, Is.EqualTo(expectedItemAndRetrievedItem).UsingRetrievedItemComparer());
        }

        [Test]
        public async Task UpdateItemAsync_WithItemNotFromDb_ReturnsRetrievedItemEmpty()
        {
            var item = ItemsCreator
                .CreateItem(id: "00000000-0000-0000-0000-000000000007", text: "different text");
            _getItemService.GetItemAsync(item.Id).Returns(Task.FromResult(RetrievedItem<Item>.Empty));

            var result = await _updateItemService.UpdateItemAsync(item);

            Assert.That(result, Is.EqualTo(RetrievedItem<Item>.Empty));
        }

        private void ArrangeTestAccordingToItem(Item expectedItem, Item itemForUpdate)
        {
            _timeManager.GetCurrentTime().Returns(expectedItem.LastUpdateTime);
            _getItemService.GetItemAsync(itemForUpdate.Id).Returns(Task.FromResult(new RetrievedItem<Item>(itemForUpdate)));
            _repository
                .UpdateItemAsync(ArgWrapper.IsItem(expectedItem))
                .Returns(Task.FromResult(expectedItem));
        }

        private static (RetrievedItem<Item> expectedItem, Item itemForUpdate) GetExpectedItemAndItemForUpdate()
        {
            const string lastUpdateTime = "2018-10-17";
            const string id = "00000000-0000-0000-0000-000000000006";
            const string text = "text";

            var itemForUpdate = ItemsCreator.CreateItem(id: id, text: text);

            var expectedItem = ItemsCreator
                .CreateItemAndRetrievedItem(id: id, text: text, lastUpdateTime: lastUpdateTime)
                .retrievedItem;

            return (expectedItem, itemForUpdate);
        }
    }
}