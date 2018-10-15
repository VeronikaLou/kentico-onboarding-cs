using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface IItemCreatorService
    {
        Task<Item> CreateItemAsync(Item item);
    }
}