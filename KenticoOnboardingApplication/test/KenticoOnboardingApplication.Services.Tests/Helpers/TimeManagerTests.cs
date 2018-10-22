using System;
using System.Collections.Generic;
using System.Threading;
using KenticoOnboardingApplication.Services.Helpers;
using NUnit.Framework;

namespace KenticoOnboardingApplication.Services.Tests.Helpers
{
    [TestFixture]
    public class TimeManagerTests
    {
        private readonly TimeManager _timeManager = new TimeManager();

        private static readonly IEnumerable<DateTime> s_minMax = new List<DateTime>
        {
            DateTime.MaxValue,
            DateTime.MinValue
        };

        [Test]
        [TestCaseSource(nameof(s_minMax))]
        public void GetDateTimeNow_DoesNotReturnMinMax(DateTime time)
        {
            var result = _timeManager.GetDateTimeNow();

            Assert.That(result, !Is.EqualTo(time));
        }

        [Test]
        public void GetDateTimeNow_ReturnsDifferentValuesForDifferentCalls()
        {
            var first = _timeManager.GetDateTimeNow().Ticks;
            Thread.Sleep(1);
            var second = _timeManager.GetDateTimeNow().Ticks;

            Assert.That(first, Is.EqualTo(second).Within(100000));
        }

        [Test]
        public void GetDateTimeNow_LaterCallReturnsLaterTime()
        {
            var first = _timeManager.GetDateTimeNow();
            Thread.Sleep(1);
            var second = _timeManager.GetDateTimeNow();

            var result = second > first;

            Assert.That(result, Is.True);
        }
    }
}