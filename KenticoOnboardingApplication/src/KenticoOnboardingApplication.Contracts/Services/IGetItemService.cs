using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface IGetItemService
    {
        Task<RetrievedItem> GetItemAsync(Guid id);
    }
}