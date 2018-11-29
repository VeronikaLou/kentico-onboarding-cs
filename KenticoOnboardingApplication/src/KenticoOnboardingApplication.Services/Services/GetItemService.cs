using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;

namespace KenticoOnboardingApplication.Services.Services
{
    internal class GetItemService : IGetItemService
    {
        private readonly IListRepository _repository;

        public GetItemService(IListRepository repository) => _repository = repository;

        public async Task<RetrievedItem<Item>> GetItemAsync(Guid id)
        {
            var databaseItem = await _repository.GetItemAsync(id);

            return databaseItem == null
                ? RetrievedItem<Item>.Null
                : new RetrievedItem<Item>(databaseItem);
        }
    }
}