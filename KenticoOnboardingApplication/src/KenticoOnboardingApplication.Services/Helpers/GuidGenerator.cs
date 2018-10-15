using System;
using KenticoOnboardingApplication.Contracts.Helpers;

namespace KenticoOnboardingApplication.Services.Helpers
{
    internal class GuidGenerator: IGuidGenerator
    {
        public Guid GenerateId() => Guid.NewGuid();
    }
}
