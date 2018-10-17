using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Api.Tests.Comparers;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;
using KenticoOnboardingApplication.Services.Services;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Api.Tests.Services
{
    [TestFixture]
    public class ItemUpdaterServiceTests
    {
        private IListRepository _repository;
        private UpdateItemService _itemUpdaterService;
        private IGetItemService _itemGetterService;
        private ITimeManager _timeManager;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _timeManager = Substitute.For<ITimeManager>();
            _itemGetterService = Substitute.For<IGetItemService>();
            _itemUpdaterService = new UpdateItemService(_repository, _timeManager, _itemGetterService);
        }

        [Test]
        public async Task UpdateItemAsync_WithItemFromDb_ReturnsItemAndTrue()
        {
            var lastUpdateTime = new DateTime(2018, 10, 17, 9, 0, 0);
            var (expectedItem, itemForUpdate) = GetExpectedItemAndItemForUpdate(lastUpdateTime);
            _timeManager.GetDateTimeNow().Returns(lastUpdateTime);
            _itemGetterService.GetItemAsync(itemForUpdate.Id).Returns(new RetrievedItem(itemForUpdate));
            _repository
                .UpdateItemAsync(Arg.Is<Item>(comparedItem => ComparerWraper.AreItemsEqual(comparedItem, expectedItem)))
                .Returns(expectedItem);

            var result = await _itemUpdaterService.UpdateItemAsync(itemForUpdate);

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

            var result = await _itemUpdaterService.UpdateItemAsync(item);

            Assert.That(result.Item, Is.Null);
            Assert.That(result.WasFound, Is.False);
        }

        private static (Item expectedItem, Item itemForUpdate) GetExpectedItemAndItemForUpdate(DateTime lastUpdateTime)
        {
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