using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIfAppveyorWorks()
        {
            var expectedResult = 2;

            var result = 1 + 1;

            Assert.AreEqual(expectedResult, result);
        }
    }
}
