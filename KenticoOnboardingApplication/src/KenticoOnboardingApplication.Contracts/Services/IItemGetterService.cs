using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface IItemGetterService
    {
        Task<ItemWrapper> GetItemAsync(Guid id);
    }
}