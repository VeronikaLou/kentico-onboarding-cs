using System;

namespace KenticoOnboardingApplication.Contracts.Helpers
{
    public interface IUrlLocator
    {
        Uri GetUri(Guid id);
    }
}