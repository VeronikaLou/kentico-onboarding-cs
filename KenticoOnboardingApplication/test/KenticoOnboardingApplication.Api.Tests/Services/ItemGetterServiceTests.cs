using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Api.Tests.Comparers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Services.Services;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Api.Tests.Services
{
    [TestFixture]
    public class ItemGetterServiceTests
    {
        private IListRepository _repository;
        private GetItemService _itemGetterService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _itemGetterService = new GetItemService(_repository);
        }

        [Test]
        public async Task GetItemAsync_WithIdFromDb_ReturnsItemAndTrue()
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
            _repository.GetItemAsync(id).Returns(expectedItem);

            var result = await _itemGetterService.GetItemAsync(id);

            Assert.That(result.Item, Is.EqualTo(expectedItem).UsingItemComparer());
            Assert.That(result.WasFound, Is.True);
        }

        [Test]
        public async Task GetItemAsync_WithIdNotFromDb_ReturnsNullAndFalse()
        {
            var id = new Guid("00000000-0000-0000-0000-000000000005");

            var result = await _itemGetterService.GetItemAsync(id);

            Assert.That(result.Item, Is.Null);
            Assert.That(result.WasFound, Is.False);
        }
    }
}