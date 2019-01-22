using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Api.Helpers;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Contracts.Services;
using Microsoft.Web.Http;

namespace KenticoOnboardingApplication.Api.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/List")]
    [Route]
    public class ListController : ApiController
    {
        private readonly IListRepository _repository;
        private readonly IUrlLocator _urlLocator;
        private readonly IGetItemService _getItemService;

        public ListController(IListRepository repository, IUrlLocator locator, IGetItemService getItemService)
        {
            _repository = repository;
            _urlLocator = locator;
            _getItemService = getItemService;
        }

        public async Task<IHttpActionResult> GetAllItemsAsync() =>
            Ok(await _repository.GetAllItemsAsync());

        [Route("{id:guid}", Name = UrlLocator.RouteGet)]
        public async Task<IHttpActionResult> GetItemAsync(Guid id)
        {
            ValidateNonEmptyId(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _getItemService.GetItemAsync(id);
            if (!result.WasFound)
            {
                return NotFound();
            }

            return Ok(result.Item);
        }

        public async Task<IHttpActionResult> PostItemAsync([FromBody] Item value)
        {
            var uri = _urlLocator.GetListItemUri(value.Id);
            var item = await _repository.AddItemAsync(value);

            return Created(uri, item);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutItemAsync(Guid id, [FromBody] Item value) =>
            Ok(await _repository.UpdateItemAsync(value));

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteItemAsync(Guid id)
        {
            await _repository.DeleteItemAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private void ValidateNonEmptyId(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(nameof(Item.Id), "Item's id must not be empty.");
            }
        }
    }
}