using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Services.Services;
using KenticoOnboardingApplication.Tests.Base.Comparers;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Services.Tests
{
    [TestFixture]
    public class CreateItemTests
    {
        private CreateItemService _createItemService;
        private IListRepository _repository;
        private ITimeManager _timeManager;
        private IIdGenerator<Guid> _guidGenerator;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _timeManager = Substitute.For<ITimeManager>();
            _guidGenerator = Substitute.For<IIdGenerator<Guid>>();
            _createItemService = new CreateItemService(_repository, _guidGenerator, _timeManager);
        }

        [Test]
        public async Task CreateItemAsync_WithValidItem_ReturnsItemWithIdAndCreationAndLastUpdate()
        {
            var expectedItem = GetExpectedItem();
            ArrangeTestAccoringToItem(expectedItem);
            var postItem = new Item {Text = "text"};

            var result = await _createItemService.CreateItemAsync(postItem);

            Assert.That(result, Is.EqualTo(expectedItem).UsingItemComparer());
        }

        private void ArrangeTestAccoringToItem(Item expectedItem)
        {
            _timeManager.GetCurrentTime().Returns(expectedItem.CreationTime);
            _guidGenerator.GenerateId().Returns(expectedItem.Id);
            _repository.AddItemAsync(Arg.Is<Item>(item => ComparerWraper.AreItemsEqual(item, expectedItem)))
                .Returns(expectedItem);
        }

        private static Item GetExpectedItem()
        {
            var creationTime = new DateTime(1999, 9, 9, 4, 22, 33);
            var guid = new Guid("00000000-0000-0000-0000-000000000006");
            var expectedItem = new Item
            {
                Id = guid,
                Text = "text",
                CreationTime = creationTime,
                LastUpdateTime = creationTime
            };

            return expectedItem;
        }
    }
}