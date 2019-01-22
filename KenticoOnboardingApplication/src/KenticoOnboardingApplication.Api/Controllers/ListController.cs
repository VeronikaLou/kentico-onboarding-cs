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
        private readonly ICreateItemService _createItemService;
        private readonly IUpdateItemService _updateItemService;

        public ListController(IListRepository repository, IUrlLocator locator, IGetItemService getItemService,
            ICreateItemService createItemService, IUpdateItemService updateItemService)
        {
            _repository = repository;
            _urlLocator = locator;
            _getItemService = getItemService;
            _createItemService = createItemService;
            _updateItemService = updateItemService;
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
            ValidateTextAndDateTimes(value);
            ValidateEmptyId(value.Id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _createItemService.CreateItemAsync(value);
            var uri = _urlLocator.GetListItemUri(item.Id);

            return Created(uri, item);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutItemAsync(Guid id, [FromBody] Item value)
        {
            ValidateTextAndDateTimes(value);
            ValidateNonEmptyId(value.Id);
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


        private void ValidateEmptyId(Guid id)
        {
            if (id != Guid.Empty)
            {
                ModelState.AddModelError(nameof(Item.Id), "Item's id must be empty.");
            }
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
                ModelState.AddModelError(nameof(Item.Text), "Item's text must not be empty.");
            }
        }

        private void ValidateCreationTime(DateTime time)
        {
            if (time != DateTime.MinValue)
            {
                ModelState.AddModelError(nameof(Item.CreationTime), "The time of item's creation must not be set.");
            }
        }

        private void ValidateLastUpdateTime(DateTime time)
        {
            if (time != DateTime.MinValue)
            {
                ModelState.AddModelError(nameof(Item.LastUpdateTime), "The time of item's last update must not be set.");
            }
        }
    }
}