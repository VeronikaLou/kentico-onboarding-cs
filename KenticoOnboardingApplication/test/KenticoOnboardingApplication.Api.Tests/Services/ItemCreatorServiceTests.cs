using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Api.Tests.Comparers;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Services.Services;
using NSubstitute;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Api.Tests.Services
{
    [TestFixture]
    public class ItemCreatorServiceTests
    {
        private CreateItemService _itemCreatorService;
        private IListRepository _repository;
        private ITimeManager _timeManager;
        private IGuidGenerator _guidGenerator;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _timeManager = Substitute.For<ITimeManager>();
            _guidGenerator = Substitute.For<IGuidGenerator>();
            _itemCreatorService = new CreateItemService(_repository, _guidGenerator, _timeManager);
        }

        [Test]
        public async Task CreateItemAsync_WithValidItem_ReturnsItemWithIdAndCreationAndLastUpdate()
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

            _timeManager.GetDateTimeNow().Returns(creationTime);
            _guidGenerator.GenerateId().Returns(guid);
            _repository.AddItemAsync(Arg.Is<Item>(item => ComparerWraper.AreItemsEqual(item, expectedItem)))
                .Returns(expectedItem);
            var postItem = new Item {Text = "text"};

            var result = await _itemCreatorService.CreateItemAsync(postItem);

            Assert.That(result, Is.EqualTo(expectedItem).UsingItemComparer());
        }
    }
}