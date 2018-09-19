using System;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Interfaces
{
    public interface IListRepository
    {
        Task<Item[]> GetAllItems();
        Task<Item> GetItem(Guid id);
        Task<Item> PostItem(Item item);
        Task<Item> PutItem(Guid id, Item item);
        Task DeleteItem(Guid id);
    }
}