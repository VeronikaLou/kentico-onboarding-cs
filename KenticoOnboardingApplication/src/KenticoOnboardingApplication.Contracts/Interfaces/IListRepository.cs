using System;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Contracts.Interfaces
{
    public interface IListRepository
    {
        Task<IHttpActionResult> GetAllItems();
        Task<IHttpActionResult> GetItem(Guid id);
        Task<IHttpActionResult> PostItem(Item item);
        Task<IHttpActionResult> PutItem(Guid id, Item item);
        Task<IHttpActionResult> DeleteItem(Guid id);
    }
}