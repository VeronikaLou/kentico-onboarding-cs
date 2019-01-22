using System;
using KenticoOnboardingApplication.Contracts.Helpers;

namespace KenticoOnboardingApplication.Services.Helpers
{
    internal class GuidGenerator : IIdGenerator<Guid>
    {
        public Guid GenerateId() => Guid.NewGuid();
    }
}