using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface IItemUpdaterService
    {
        Task<ItemWrapper> UpdateItemAsync(Item item);
    }
}