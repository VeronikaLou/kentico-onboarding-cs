using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Contracts;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using Microsoft.Web.Http;

namespace KenticoOnboardingApplication.Api.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/List")]
    public class ListController : ApiController
    {
        private readonly IListRepository _repository;
        private readonly IUrlLocator _urlLocator;

        public ListController(IListRepository repository, IUrlLocator locator)
        {
            _repository = repository;
            _urlLocator = locator;
        }

        [Route]
        public async Task<IHttpActionResult> GetAllItemsAsync() =>
            Ok(await _repository.GetAllItemsAsync());

        [Route("{id:guid}", Name = "Get")]
        public async Task<IHttpActionResult> GetItemAsync(Guid id) =>
            Ok(await _repository.GetItemAsync(id));

        [Route]
        public async Task<IHttpActionResult> PostItemAsync([FromBody] Item value)
        {
            var uri = _urlLocator.GetUri(value.Id);

            return Created(uri, await _repository.AddItemAsync(value));
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutItemAsync(Guid id, [FromBody] Item value) =>
            Ok(await _repository.UpdateItemAsync(id, value));

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteItemAsync(Guid id)
        {
            await _repository.DeleteItemAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}