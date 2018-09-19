using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts
{
    public interface IListRepository
    {
        Task<Item[]> GetAllItems();
        Task<Item> GetItem(Guid id);
        Task<Item> AddItem(Item item);
        Task<Item> UpdateItem(Guid id, Item item);
        Task DeleteItem(Guid id);
    }
}