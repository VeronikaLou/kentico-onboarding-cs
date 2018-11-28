using System;

namespace KenticoOnboardingApplication.Contracts.Helpers
{
    public interface IIdGenerator
    {
        Guid GenerateId();
    }
}