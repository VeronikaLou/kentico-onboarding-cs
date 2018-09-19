using System;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Contracts.Interfaces;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.ListRepository
{
    public class ListRepository : IListRepository
    {
        public Task<IHttpActionResult> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> PostItem(Item item)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> PutItem(Guid id, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
