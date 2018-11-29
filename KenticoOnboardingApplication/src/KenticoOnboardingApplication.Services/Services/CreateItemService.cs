using System;
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
        private readonly IIdGenerator<Guid> _guidGenerator;
        private readonly ITimeManager _timeManager;

        public CreateItemService(IListRepository repository, IIdGenerator<Guid> guidGenerator, ITimeManager timeManager)
        {
            _repository = repository;
            _guidGenerator = guidGenerator;
            _timeManager = timeManager;
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            var newItem = CreateItem(item.Text);

            return await _repository.AddItemAsync(newItem);
        }

        public Item CreateItem(string text)
        {
            var currentTime = _timeManager.GetCurrentTime();
            var id = _guidGenerator.GenerateId();

            return new Item
            {
                Id = id,
                Text = text,
                CreationTime = currentTime,
                LastUpdateTime = currentTime
            };
        }
    }
}