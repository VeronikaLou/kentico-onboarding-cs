using System;
using KenticoOnboardingApplication.Contracts.Helpers;

namespace KenticoOnboardingApplication.Services.Helpers
{
    internal class TimeManager : ITimeManager
    {
        public DateTime GetCurrentTime() => DateTime.UtcNow;
    }
}