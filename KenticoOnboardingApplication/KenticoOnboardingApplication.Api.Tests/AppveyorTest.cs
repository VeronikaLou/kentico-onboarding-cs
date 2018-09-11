using NUnit.Framework;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class AppveyorTest
    {
        [Test]
        public void TestIfAppveyorWorks()
        {
            var expectedResult = 2;

            var result = 1 + 1;

            Assert.AreEqual(expectedResult, result);
        }
    }
}
