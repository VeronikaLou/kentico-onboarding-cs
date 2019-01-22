using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;
using KenticoOnboardingApplication.Contracts.Services.Wrappers;

namespace KenticoOnboardingApplication.Services.Services
{
    internal class UpdateItemService : IUpdateItemService
    {
        private readonly IListRepository _repository;
        private readonly ITimeManager _timeManager;
        private readonly IGetItemService _getItemService;

        public UpdateItemService(IListRepository repository, ITimeManager timeManager, IGetItemService getItemService)
        {
            _repository = repository;
            _timeManager = timeManager;
            _getItemService = getItemService;
        }

        public async Task<RetrievedItem<Item>> UpdateItemAsync(Item item)
        {
            var retrievedItem = await _getItemService.GetItemAsync(item.Id);
            if (!retrievedItem.WasFound)
            {
                return retrievedItem;
            }

            var updatedItem = CreateUpdatedItem(retrievedItem, item);
            var result = await _repository.UpdateItemAsync(updatedItem);

            return result == null
                ? RetrievedItem<Item>.Empty
                : new RetrievedItem<Item>(result);
        }

        private Item CreateUpdatedItem(RetrievedItem<Item> existingItem, Item newItem)
        {
            var currentTime = _timeManager.GetCurrentTime();

            return new Item
            {
                Id = newItem.Id,
                Text = newItem.Text,
                CreationTime = existingItem.Item.CreationTime,
                LastUpdateTime = currentTime
            };
        }
    }
}