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
    public class ItemUpdaterServiceTests
    {
        private IListRepository _repository;
        private ItemUpdaterService _itemUpdaterService;
        private ITimeManager _timeManager;

        private readonly Item _beforeUpdate = new Item
        {
            Id = new Guid("00000000-0000-0000-0000-000000000006"),
            Text = "text",
            CreationTime = DateTime.MinValue,
            LastUpdateTime = DateTime.MinValue
        };

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IListRepository>();
            _timeManager = Substitute.For<ITimeManager>();
            _itemUpdaterService = new ItemUpdaterService(_repository, _timeManager);
        }

        [Test]
        public async Task UpdateItemAsync_WithItemFromDb_ReturnsItemAndTrue()
        {
            var afterUpdate = _beforeUpdate;
            afterUpdate.LastUpdateTime = DateTime.MaxValue;
            _timeManager.GetDateTimeNow().Returns(DateTime.MaxValue);
            _repository.GetItemAsync(_beforeUpdate.Id).Returns(_beforeUpdate);
            _repository.UpdateItemAsync(_beforeUpdate).Returns(afterUpdate);

            var result = await _itemUpdaterService.UpdateItemAsync(_beforeUpdate);

            Assert.That(result.Item, Is.EqualTo(afterUpdate).UsingItemComparer());
            Assert.That(result.WasFound, Is.True);
        }

        [Test]
        public async Task UpdateItemAsync_WithItemNotFromDb_ReturnsNullAdnFalse()
        {
            var result = await _itemUpdaterService.UpdateItemAsync(_beforeUpdate);

            Assert.That(result.Item, Is.Null);
            Assert.That(result.WasFound, Is.False);
        }
    }
}