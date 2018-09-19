using System;

namespace KenticoOnboardingApplication.Contracts
{
    public interface IUrlLocator
    {
        Uri GetUri(Guid id);
    }
}