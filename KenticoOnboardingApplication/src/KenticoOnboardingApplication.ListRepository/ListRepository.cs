using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.ListRepository
{
    internal class ListRepository : IListRepository
    {
        private static readonly Item[] Items =
        {
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000001"), Text = "Learn C#"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000002"), Text = "Create dummy controller"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000003"), Text = "Connect JS and TS"}
        };

        public async Task<IEnumerable<Item>> GetAllItemsAsync() => await Task.FromResult(Items);

        public async Task<Item> GetItemAsync(Guid id) => await Task.FromResult(Items[0]);

        public async Task<Item> AddItemAsync(Item item) => await Task.FromResult(Items[1]);

        public async Task<Item> UpdateItemAsync(Item item) => await Task.FromResult(Items[0]);

        public async Task DeleteItemAsync(Guid id) => await Task.CompletedTask;
    }
}