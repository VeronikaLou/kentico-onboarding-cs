using System;
using KenticoOnboardingApplication.Contracts.Helpers;

namespace KenticoOnboardingApplication.Services.Helpers
{
    internal class GuidGenerator: IIdGenerator
    {
        public Guid GenerateId() => Guid.NewGuid();
    }
}
