using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;

namespace KenticoOnboardingApplication.Services.Services
{
    internal class ItemGetterService : IItemGetterService
    {
        private readonly IListRepository _repository;

        public ItemGetterService(IListRepository respository) => _repository = respository;

        public async Task<ItemWrapper> GetItemAsync(Guid id)
        {
            var databaseItem = await _repository.GetItemAsync(id);

            return new ItemWrapper(databaseItem);
        }
    }
}