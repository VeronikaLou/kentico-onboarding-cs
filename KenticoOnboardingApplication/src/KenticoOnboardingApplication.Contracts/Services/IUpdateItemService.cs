using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface IUpdateItemService
    {
        Task<RetrievedItem<Item>> UpdateItemAsync(Item item);
    }
}