using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Services.Services;
using KenticoOnboardingApplication.Tests.Base.Comparers;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Services.Tests
{
    [TestFixture]
    public class ItemGetterServiceTests
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
            var expectedItem = GetExpectedItem();
            var id = expectedItem.Id;
            _repository.GetItemAsync(id).Returns(expectedItem);

            var result = await _getItemService.GetItemAsync(id);

            Assert.That(result.Item, Is.EqualTo(expectedItem).UsingItemComparer());
            Assert.That(result.WasFound, Is.True);
        }

        [Test]
        public async Task GetItemAsync_WithIdNotFromDb_ReturnsNullAndFalse()
        {
            var id = new Guid("00000000-0000-0000-0000-000000000005");

            var result = await _getItemService.GetItemAsync(id);

            Assert.That(result.Item, Is.Null);
            Assert.That(result.WasFound, Is.False);
        }

        private static Item GetExpectedItem()
        {
            var id = new Guid("00000000-0000-0000-0000-000000000006");
            var dateTime = new DateTime(2000, 12, 20, 21, 59, 59);
            var expectedItem = new Item
            {
                Id = id,
                Text = "text",
                CreationTime = dateTime,
                LastUpdateTime = dateTime
            };

            return expectedItem;
        }
    }
}