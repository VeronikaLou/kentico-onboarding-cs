using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using MongoDB.Driver;

namespace KenticoOnboardingApplication.Repositories.Repositories
{
    internal class ListRepository : IListRepository
    {
        private static IMongoCollection<Item> _collection;

        public ListRepository(IConnectionString connectionString)
        {
            var urlConnectionString = MongoUrl.Create(connectionString.TodoList);
            var client = new MongoClient(urlConnectionString);
            var db = client.GetDatabase(urlConnectionString.DatabaseName);
            _collection = db.GetCollection<Item>("items");
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
            await _collection.Find(FilterDefinition<Item>.Empty).ToListAsync();

        public async Task<Item> GetItemAsync(Guid id) =>
            await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task<Item> AddItemAsync(Item item)
        {
            await _collection.InsertOneAsync(item);

            return item;
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            await _collection.FindOneAndReplaceAsync(foundItem => foundItem.Id == item.Id, item);

            return item;
        }

        public async Task DeleteItemAsync(Guid id) =>
            await _collection.DeleteOneAsync(item => item.Id == id);
    }
}