using System;
using System.Threading.Tasks;
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
        private ItemCreatorService _itemCreatorService;
        private IListRepository _repository;
        private ITimeManager _timeManager;
        private IGuidGenerator _guidGenerator;

        private readonly Item _expectedItem = new Item
        {
            Id = new Guid("00000000-0000-0000-0000-000000000006"),
            Text = "text",
            CreationTime = DateTime.MaxValue,
            LastUpdateTime = DateTime.MaxValue
        };

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _timeManager = Substitute.For<ITimeManager>();
            _guidGenerator = Substitute.For<IGuidGenerator>();
            _itemCreatorService = new ItemCreatorService(_repository, _guidGenerator, _timeManager);
        }

        [Test]
        public async Task CreateItemAsync_WithValidItem_ReturnsItemWithIdAndCreationAndLastUpdate()
        {
            _timeManager.GetDateTimeNow().Returns(DateTime.MaxValue);
            _guidGenerator.GenerateId().Returns(new Guid("00000000-0000-0000-0000-000000000006"));
            _repository.AddItemAsync(Arg.Is<Item>(item => IsItemCorrect(item))).Returns(_expectedItem);
            var postItem = new Item {Text = "text"};

            var result = await _itemCreatorService.CreateItemAsync(postItem);

            Assert.That(result.Id, Is.EqualTo(_expectedItem.Id));
            Assert.That(result.CreationTime, Is.EqualTo(_expectedItem.CreationTime));
            Assert.That(result.LastUpdateTime, Is.EqualTo(_expectedItem.LastUpdateTime));
            Assert.That(result.Text, Is.EqualTo(_expectedItem.Text));
            Assert.That(result, Is.EqualTo(_expectedItem));
        }

        private bool IsItemCorrect(Item item) =>
            item.Id == _expectedItem.Id && item.Text == _expectedItem.Text &&
            item.CreationTime == _expectedItem.CreationTime &&
            item.LastUpdateTime == _expectedItem.LastUpdateTime;
    }
}