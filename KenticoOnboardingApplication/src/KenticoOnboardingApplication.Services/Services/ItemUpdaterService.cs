using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;

namespace KenticoOnboardingApplication.Services.Services
{
    internal class ItemUpdaterService : IItemUpdaterService
    {
        private readonly IListRepository _repository;
        private readonly ITimeManager _timeManager;
        private readonly IItemGetterService _itemGetterService;

        public ItemUpdaterService(IListRepository repository, ITimeManager timeManager, IItemGetterService itemGetterService)
        {
            _repository = repository;
            _timeManager = timeManager;
            _itemGetterService = itemGetterService;
        }

        public async Task<RetrievedItem> UpdateItemAsync(Item item)
        {
            var databaseItem = await _itemGetterService.GetItemAsync(item.Id);
            if (!databaseItem.WasFound)
                return databaseItem;

            var updatedItem = new Item
            {
                Id = item.Id,
                Text = item.Text,
                CreationTime = databaseItem.Item.CreationTime,
                LastUpdateTime = _timeManager.GetDateTimeNow()
            };
            var result = await _repository.UpdateItemAsync(updatedItem);

            return new RetrievedItem(result);
        }
    }
}