using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;

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

            var updatedItem = CreateUpdatedItem(item.Id, item.Text, retrievedItem.Item.CreationTime);
            var result = await _repository.UpdateItemAsync(updatedItem);

            return result == null
                ? RetrievedItem<Item>.Null
                : new RetrievedItem<Item>(result);
        }

        private Item CreateUpdatedItem(Guid id, string text, DateTime creationTime)
        {
            var currentTime = _timeManager.GetCurrentTime();

            return new Item
            {
                Id = id,
                Text = text,
                CreationTime = creationTime,
                LastUpdateTime = currentTime
            };
        }
    }
}