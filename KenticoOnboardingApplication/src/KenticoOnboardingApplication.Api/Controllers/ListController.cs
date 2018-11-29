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
        private readonly IUpdateItemService _updateItemService;
        private readonly ICreateItemService _createItemService;
        private readonly IGetItemService _getItemService;

        public ListController(IListRepository repository, IUrlLocator locator, ICreateItemService createItemService,
            IUpdateItemService updateItemService, IGetItemService getItemService)
        {
            _repository = repository;
            _urlLocator = locator;
            _createItemService = createItemService;
            _updateItemService = updateItemService;
            _getItemService = getItemService;
        }

        public async Task<IHttpActionResult> GetAllItemsAsync() =>
            Ok(await _repository.GetAllItemsAsync());

        [Route("{id:guid}", Name = UrlLocator.RouteGet)]
        public async Task<IHttpActionResult> GetItemAsync(Guid id)
        {
            ValidateId(id, shouldBeEmpty: false);
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
            ValidateTextAndDateTimes(value);
            ValidateId(value.Id, shouldBeEmpty: true);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uri = _urlLocator.GetListItemUri(value.Id);
            var item = await _createItemService.CreateItemAsync(value);

            return Created(uri, item);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutItemAsync(Guid id, [FromBody] Item value)
        {
            ValidateTextAndDateTimes(value);
            ValidateId(value.Id, shouldBeEmpty: false);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _updateItemService.UpdateItemAsync(value);
            if (!result.WasFound)
            {
                value.Id = Guid.Empty;
                return await PostItemAsync(value);
            }

            return Ok(result.Item);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteItemAsync(Guid id)
        {
            ValidateId(id, shouldBeEmpty: false);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var retrievedItem = await _getItemService.GetItemAsync(id);
            if (!retrievedItem.WasFound)
            {
                return NotFound();
            }

            await _repository.DeleteItemAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private void ValidateTextAndDateTimes(Item item)
        {
            ValidateText(item.Text);
            ValidateLastUpdateTime(item.LastUpdateTime);
            ValidateCreationTime(item.CreationTime);
        }

        private void ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                ModelState.AddModelError(nameof(Item.Text), "Text cannot be empty.");
            }
        }

        private void ValidateCreationTime(DateTime time)
        {
            if (time != DateTime.MinValue)
            {
                ModelState.AddModelError(nameof(Item.CreationTime), "CreationTime cannot be set.");
            }
        }

        private void ValidateLastUpdateTime(DateTime time)
        {
            if (time != DateTime.MinValue)
            {
                ModelState.AddModelError(nameof(Item.LastUpdateTime), "LastUpdateTime cannot be set.");
            }
        }

        private void ValidateId(Guid id, bool shouldBeEmpty)
        {
            if (shouldBeEmpty)
            {
                ValidateEmptyId(id);
            }
            else
            {
                ValidateNonEmptyId(id);
            }
        }

        private void ValidateEmptyId(Guid id)
        {
            if (id != Guid.Empty)
            {
                ModelState.AddModelError(nameof(Item.Id), "Id must be empty.");
            }
        }

        private void ValidateNonEmptyId(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(nameof(Item.Id), "Id cannot be empty.");
            }
        }
    }
}