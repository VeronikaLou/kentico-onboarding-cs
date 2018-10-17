using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface ICreateItemService
    {
        Task<Item> CreateItemAsync(Item item);
    }
}