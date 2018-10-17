using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface IItemGetterService
    {
        Task<RetrievedItem> GetItemAsync(Guid id);
    }
}