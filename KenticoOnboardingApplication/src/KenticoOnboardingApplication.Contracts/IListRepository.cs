using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts
{
    public interface IListRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemAsync(Guid id);
        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(Guid id, Item item);
        Task DeleteItemAsync(Guid id);
    }
}