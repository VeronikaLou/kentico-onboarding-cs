using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;

namespace KenticoOnboardingApplication.Services.Services
{
    internal class ItemUpdaterService: IItemUpdaterService
    {
        private readonly IListRepository _repository;
        private readonly ITimeManager _timeManager;

        public ItemUpdaterService(IListRepository repository, ITimeManager timeManager)
        {
            _repository = repository;
            _timeManager = timeManager;
        }

        public async Task<ItemWrapper> UpdateItemAsync(Item item)
        {
            var databaseItem = await _repository.GetItemAsync(item.Id);
            if (databaseItem == null)
                return new ItemWrapper(null);
           
            item.CreationTime = databaseItem.CreationTime;
            item.LastUpdateTime = _timeManager.GetDateTimeNow();
            var result = await _repository.UpdateItemAsync(item);

            return new ItemWrapper(result);
        }
    }
}