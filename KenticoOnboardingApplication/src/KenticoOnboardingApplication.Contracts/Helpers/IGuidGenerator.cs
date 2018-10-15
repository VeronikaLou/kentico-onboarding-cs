using System;

namespace KenticoOnboardingApplication.Contracts.Helpers
{
    public interface IGuidGenerator
    {
        Guid GenerateId();
    }
}