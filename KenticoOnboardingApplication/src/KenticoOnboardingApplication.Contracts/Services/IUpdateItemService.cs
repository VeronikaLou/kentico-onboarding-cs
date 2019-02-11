using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Services.Wrappers;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface IUpdateItemService
    {
        Task<RetrievedItem<Item>> UpdateItemAsync(Item item);
    }
}