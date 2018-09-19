using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Contracts;
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
        public async Task<IHttpActionResult> GetAllItems() =>
            Ok(await _repository.GetAllItems());

        [Route("{id:guid}", Name = "Get")]
        public async Task<IHttpActionResult> GetItem(Guid id) =>
            Ok(await _repository.GetItem(id));

        [Route]
        public async Task<IHttpActionResult> PostItem([FromBody] Item value)
        {
            var uri = _urlLocator.GetUri(value.Id);

            return Created(uri, await _repository.AddItem(value));
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutItem(Guid id, [FromBody] Item value) =>
            Ok(await _repository.UpdateItem(id, value));

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteItem(Guid id)
        {
            await _repository.DeleteItem(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}