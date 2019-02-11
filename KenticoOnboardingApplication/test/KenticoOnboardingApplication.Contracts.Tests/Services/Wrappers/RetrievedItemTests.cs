using System;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Services.Wrappers;
using KenticoOnboardingApplication.Tests.Base.Factories;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Contracts.Tests.Services.Wrappers
{
    [TestFixture]
    public class RetrievedItemTests
    {
        private readonly Item _item = ItemsCreator.CreateItem("00000000-0000-0000-0000-000000000001", "I am  item.");

        [Test]
        public void RetrievedItem_WithNullItem_ThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new RetrievedItem<Item>(item: null);
            });

        [Test]
        public void RetrievedItem_WithValidItem_ReturnsRetrievedItem()
        {
            var result = new RetrievedItem<Item>(_item);

            Assert.That(result.Item, Is.EqualTo(_item));
            Assert.That(result.WasFound, Is.True);
        }

        [Test]
        public void GetItem_RetrievedItemEmpty_ThrowsInvalidOperationException()
        {
            var emptyRetrievedItem = RetrievedItem<Item>.Empty;

            Assert.Throws<InvalidOperationException>(() =>
            {
                var _ = emptyRetrievedItem.Item;
            });
        }

        [Test]
        public void GetItem_NonEmptyRetrievedItem_ReturnsItem()
        {
            var nonEmptyRetrievedItem = new RetrievedItem<Item>(_item);

            var result = nonEmptyRetrievedItem.Item;

            Assert.That(_item, Is.EqualTo(result));
        }

        [Test]
        public void RetrievedItemEmpty_IsNotNull()
        {
            var emptyRetrievedItem = RetrievedItem<Item>.Empty;

            Assert.That(emptyRetrievedItem, Is.Not.Null);
        }

        [Test]
        public void RetrievedItemEmpty_IsSingleton()
        {
            var first = RetrievedItem<Item>.Empty;
            var second = RetrievedItem<Item>.Empty;
            var third = RetrievedItem<Item>.Empty;

            Assert.That(first, Is.EqualTo(second));
            Assert.That(second, Is.EqualTo(third));
            Assert.That(third, Is.EqualTo(first));
        }
    }
}