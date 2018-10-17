using System.Threading.Tasks;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Services
{
    public interface IUpdateItemService
    {
        Task<RetrievedItem> UpdateItemAsync(Item item);
    }
}