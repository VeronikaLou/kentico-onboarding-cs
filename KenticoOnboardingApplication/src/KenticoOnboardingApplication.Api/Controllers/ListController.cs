using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Contracts.Interfaces;
using KenticoOnboardingApplication.Contracts.Models;
using Microsoft.Web.Http;

namespace KenticoOnboardingApplication.Api.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/List")]
    public class ListController : ApiController
    {
        private readonly IListRepository _repository;

        public ListController(IListRepository repository)
        {
            _repository = repository;
        }

        [Route]
        public async Task<IHttpActionResult> GetAllItems()
        {
            return await Task.FromResult(Ok(await _repository.GetAllItems()));
        }    

        [Route("{id:guid}", Name = "Get")]
        public async Task<IHttpActionResult> GetItem(Guid id) =>
            await Task.FromResult(Ok(await _repository.GetItem(id)));

        [Route]
        public async Task<IHttpActionResult> PostItem([FromBody] Item value)
        {
            var uri = Url.Link("Get", new {id = "00000000-0000-0000-0000-000000000002"});

            return await Task.FromResult(Created(uri, await _repository.PostItem(value)));
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutItem(Guid id, [FromBody] Item value) =>
            await Task.FromResult(Ok(await _repository.PutItem(id, value)));

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteItem(Guid id) =>
            await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
    }
}