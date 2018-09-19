using System;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Contracts.Interfaces;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.ListRepository
{
    public class ListRepository : IListRepository
    {
        private static readonly Item[] Items =
        {
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000001"), Text = "Learn C#"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000002"), Text = "Create dummy controller"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000003"), Text = "Connect JS and TS"}
        };

        public async Task<Item[]> GetAllItems() => await Task.FromResult(Items);

        public async Task<Item> GetItem(Guid id) => await Task.FromResult(Items[0]);

        public async Task<Item> PostItem(Item item) => await Task.FromResult(Items[1]);

        public async Task<Item> PutItem(Guid id, Item item) => await Task.FromResult(Items[0]);

        public async Task DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}