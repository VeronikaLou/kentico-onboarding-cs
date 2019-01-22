using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services.Wrappers;
using KenticoOnboardingApplication.Services.Services;
using KenticoOnboardingApplication.Tests.Base;
using KenticoOnboardingApplication.Tests.Base.Factories;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Services.Tests.Services
{
    [TestFixture]
    public class GetItemTests
    {
        private IListRepository _repository;
        private GetItemService _getItemService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _getItemService = new GetItemService(_repository);
        }

        [Test]
        public async Task GetItemAsync_WithIdFromDb_ReturnsItemAndTrue()
        {
            var (item, retrievedItem) = GetExpectedItem();
            var id = item.Id;
            _repository.GetItemAsync(id).Returns(Task.FromResult(item));

            var result = await _getItemService.GetItemAsync(id);

            Assert.That(result, Is.EqualTo(retrievedItem).UsingRetrievedItemComparer());
        }

        [Test]
        public async Task GetItemAsync_WithIdNotFromDb_ReturnsNullAndFalse()
        {
            var id = new Guid("00000000-0000-0000-0000-000000000005");

            var result = await _getItemService.GetItemAsync(id);

            Assert.That(result.WasFound, Is.False);
        }

        private static (Item item, RetrievedItem<Item> retrievedItem) GetExpectedItem()
        {
            const string creationTime = "2000-12-20";
            var expectedItem = ItemsCreator.CreateItemAndRetrievedItem(
                id: "00000000-0000-0000-0000-000000000006",
                text: "text",
                creationTime: creationTime,
                lastUpdateTime: creationTime);

            return expectedItem;
        }
    }
}