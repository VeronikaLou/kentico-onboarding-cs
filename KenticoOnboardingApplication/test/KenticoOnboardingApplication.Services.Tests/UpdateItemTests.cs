using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;
using KenticoOnboardingApplication.Services.Services;
using KenticoOnboardingApplication.Tests.Base.Comparers;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Services.Tests
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
            var (expectedItem, itemForUpdate) = GetExpectedItemAndItemForUpdate();
            Arrange(expectedItem, itemForUpdate);

            var result = await _updateItemService.UpdateItemAsync(itemForUpdate);

            Assert.That(result.Item, Is.EqualTo(expectedItem).UsingItemComparer());
            Assert.That(result.WasFound, Is.True);
        }

        [Test]
        public async Task UpdateItemAsync_WithItemNotFromDb_ReturnsNullAdnFalse()
        {
            var item = new Item
            {
                Id = new Guid("00000000-0000-0000-0000-000000000007"),
                Text = "different text",
            };
            _getItemService.GetItemAsync(item.Id).Returns(new RetrievedItem(null));

            var result = await _updateItemService.UpdateItemAsync(item);

            Assert.That(result.Item, Is.Null);
            Assert.That(result.WasFound, Is.False);
        }

        private void Arrange(Item expectedItem, Item itemForUpdate)
        {
            _timeManager.GetDateTimeNow().Returns(expectedItem.LastUpdateTime);
            _getItemService.GetItemAsync(itemForUpdate.Id).Returns(new RetrievedItem(itemForUpdate));
            _repository
                .UpdateItemAsync(Arg.Is<Item>(comparedItem => ComparerWraper.AreItemsEqual(comparedItem, expectedItem)))
                .Returns(expectedItem);
        }

        private static (Item expectedItem, Item itemForUpdate) GetExpectedItemAndItemForUpdate()
        {
            var lastUpdateTime = new DateTime(2018, 10, 17, 9, 0, 0);
            var itemForUpdate = new Item
            {
                Id = new Guid("00000000-0000-0000-0000-000000000006"),
                Text = "text"
            };

            var expectedItem = new Item
            {
                Id = itemForUpdate.Id,
                Text = itemForUpdate.Text,
                LastUpdateTime = lastUpdateTime
            };

            return (expectedItem, itemForUpdate);
        }
    }
}