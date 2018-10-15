using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;

namespace KenticoOnboardingApplication.Services.Services
{
    internal class ItemCreatorService: IItemCreatorService
    {
        private readonly IListRepository _repository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ITimeManager _timeManager;

        public ItemCreatorService(IListRepository repository, IGuidGenerator guidGenerator, ITimeManager timeManager)
        {
            _repository = repository;
            _guidGenerator = guidGenerator;
            _timeManager = timeManager;
        }


        public async Task<Item> CreateItemAsync(Item item)
        {
            item.Id = _guidGenerator.GenerateId();
            item.CreationTime = _timeManager.GetDateTimeNow();
            item.LastUpdateTime = _timeManager.GetDateTimeNow();

            return await _repository.AddItemAsync(item);
        }
    }
}
