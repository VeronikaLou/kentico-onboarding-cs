using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using MongoDB.Driver;

namespace KenticoOnboardingApplication.Repositories.Repositories
{
    internal class ListRepository : IListRepository
    {
        private static IMongoCollection<Item> _collection;

        public ListRepository(string connectionString)
        {
            var urlConnectionString = MongoUrl.Create(connectionString);
            var client = new MongoClient(urlConnectionString);
            var db = client.GetDatabase(urlConnectionString.DatabaseName);

            _collection = db.GetCollection<Item>("items");
            _collection.InsertManyAsync(s_items);
        }

        private static readonly Item[] s_items =
        {
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000001"), Text = "Learn C#"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000002"), Text = "Create dummy controller"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000003"), Text = "Connect JS and TS"}
        };

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            var items = await _collection.FindAsync(FilterDefinition<Item>.Empty);
            return items.ToList();
        }

        public async Task<Item> GetItemAsync(Guid id) =>
            await _collection.Find(_ => _.Id == id).SingleAsync();


        public async Task<Item> AddItemAsync(Item item)
        {
            await _collection.InsertOneAsync(s_items[1]);
            return s_items[1];
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            var filter = Builders<Item>.Filter.Eq(_ => _.Id, item.Id);
            var update = Builders<Item>.Update.Set("LastUpdate", DateTime.Now).Set("Text", item.Text);
            await _collection.FindOneAndUpdateAsync(filter, update);
            return item;
        }


        public async Task DeleteItemAsync(Guid id)
        {
            var filter = Builders<Item>.Filter.Eq(_ => _.Id, id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}