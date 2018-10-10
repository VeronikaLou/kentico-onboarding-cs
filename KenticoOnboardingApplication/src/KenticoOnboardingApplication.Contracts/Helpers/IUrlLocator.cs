using System;

namespace KenticoOnboardingApplication.Contracts.Helpers
{
    public interface IUrlLocator
    {
        Uri GetListItemUri(Guid id);
    }
}