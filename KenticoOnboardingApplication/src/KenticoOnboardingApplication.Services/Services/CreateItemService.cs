using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;

namespace KenticoOnboardingApplication.Services.Services
{
    internal class CreateItemService : ICreateItemService
    {
        private readonly IListRepository _repository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ITimeManager _timeManager;

        public CreateItemService(IListRepository repository, IGuidGenerator guidGenerator, ITimeManager timeManager)
        {
            _repository = repository;
            _guidGenerator = guidGenerator;
            _timeManager = timeManager;
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            var dateTimeNow = _timeManager.GetDateTimeNow();
            var newItem = new Item
            {
                Text = item.Text,
                Id = _guidGenerator.GenerateId(),
                CreationTime = dateTimeNow,
                LastUpdateTime = dateTimeNow
            };

            return await _repository.AddItemAsync(newItem);
        }
    }
}