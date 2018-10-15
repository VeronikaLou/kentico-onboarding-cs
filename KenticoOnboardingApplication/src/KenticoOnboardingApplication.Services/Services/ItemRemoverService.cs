using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;

namespace KenticoOnboardingApplication.Services.Services
{
    internal class ItemRemoverService : IItemRemoverService
    {
        private readonly IListRepository _repository;

        public ItemRemoverService(IListRepository repository) => _repository = repository;

        public async Task<ItemWrapper> DeleteItemAsync(Guid id)
        {
            var databaseItem = await _repository.GetItemAsync(id);

            return new ItemWrapper(databaseItem);
        }
    }
}