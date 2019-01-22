using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Services.Services;
using KenticoOnboardingApplication.Tests.Base;
using KenticoOnboardingApplication.Tests.Base.Factories;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Services.Tests.Services
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
            ArrangeTestAccordingToItem(expectedItem);
            var postItem = ItemsCreator.CreateItem(text: "text");

            var result = await _createItemService.CreateItemAsync(postItem);

            Assert.That(result, Is.EqualTo(expectedItem).UsingItemComparer());
        }

        private void ArrangeTestAccordingToItem(Item expectedItem)
        {
            _timeManager.GetCurrentTime().Returns(expectedItem.CreationTime);
            _guidGenerator.GenerateId().Returns(expectedItem.Id);
            _repository.AddItemAsync(ArgWrapper.IsItem(expectedItem))
                .Returns(Task.FromResult(expectedItem));
        }

        private static Item GetExpectedItem()
        {
            const string creationTime = "1999-9-9";
            var expectedItem = ItemsCreator.CreateItem(
                id: "00000000-0000-0000-0000-000000000006",
                text: "text",
                creationTime: creationTime,
                lastUpdateTime: creationTime
            );

            return expectedItem;
        }
    }
}