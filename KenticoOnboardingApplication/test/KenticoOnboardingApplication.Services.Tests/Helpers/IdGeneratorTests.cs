using System;
using KenticoOnboardingApplication.Services.Helpers;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Services.Tests.Helpers
{
    [TestFixture]
    public class IdGeneratorTests
    {
        private readonly GuidGenerator _guidGenerator = new GuidGenerator();

        [Test]
        public void GenerateId_ReturnsDifferentIds()
        {
            var first = _guidGenerator.GenerateId();
            var second = _guidGenerator.GenerateId();

            Assert.That(first, !Is.EqualTo(second));
        }

        [Test]
        public void GenerateId_DoesNotReturnEmptyId()
        {
            var empty = Guid.Empty;

            var result = _guidGenerator.GenerateId();

            Assert.That(result, !Is.EqualTo(empty));
        }
    }
}