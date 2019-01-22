using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Services.Wrappers;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface IGetItemService
    {
        Task<RetrievedItem<Item>> GetItemAsync(Guid id);
    }
}