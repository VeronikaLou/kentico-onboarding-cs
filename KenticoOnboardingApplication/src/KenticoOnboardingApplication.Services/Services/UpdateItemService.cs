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

        public async Task<RetrievedItem> UpdateItemAsync(Item item)
        {
            var retrievedItem = await _getItemService.GetItemAsync(item.Id);
            if (!retrievedItem.WasFound)
                return retrievedItem;

            var updatedItem = new Item
            {
                Id = item.Id,
                Text = item.Text,
                CreationTime = retrievedItem.Item.CreationTime,
                LastUpdateTime = _timeManager.GetDateTimeNow()
            };
            var result = await _repository.UpdateItemAsync(updatedItem);

            return new RetrievedItem(result);
        }
    }
}