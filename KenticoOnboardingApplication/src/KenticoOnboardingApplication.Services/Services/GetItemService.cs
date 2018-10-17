﻿using System;
using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;

namespace KenticoOnboardingApplication.Services.Services
{
    internal class GetItemService : IGetItemService
    {
        private readonly IListRepository _repository;

        public GetItemService(IListRepository respository) => _repository = respository;

        public async Task<RetrievedItem> GetItemAsync(Guid id)
        {
            var databaseItem = await _repository.GetItemAsync(id);

            return new RetrievedItem(databaseItem);
        }
    }
}