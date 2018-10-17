using System;
using System.Linq;
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
        private readonly IItemUpdaterService _itemUpdaterService;
        private readonly IItemCreatorService _itemCreatorService;
        private readonly IItemGetterService _itemGetterService;

        public ListController(IListRepository repository, IUrlLocator locator, IItemCreatorService itemCreatorService,
            IItemUpdaterService itemUpdaterService, IItemGetterService itemGetterService)
        {
            _repository = repository;
            _urlLocator = locator;
            _itemCreatorService = itemCreatorService;
            _itemUpdaterService = itemUpdaterService;
            _itemGetterService = itemGetterService;
        }

        public async Task<IHttpActionResult> GetAllItemsAsync() =>
            Ok(await _repository.GetAllItemsAsync());

        [Route("{id:guid}", Name = UrlLocator.RouteGet)]
        public async Task<IHttpActionResult> GetItemAsync(Guid id)
        {
            ShouldBeIdEmpty(id, false);
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessage());
            var result = await _itemGetterService.GetItemAsync(id);
            if (!result.WasFound)
                return NotFound();

            return Ok(result.Item);
        }

        public async Task<IHttpActionResult> PostItemAsync([FromBody] Item value)
        {
            ValidateTextAndDateTimes(value);
            ShouldBeIdEmpty(value.Id, true);
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessage());
            var uri = _urlLocator.GetListItemUri(value.Id);
            var item = await _itemCreatorService.CreateItemAsync(value);

            return Created(uri, item);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutItemAsync(Guid id, [FromBody] Item value)
        {
            ValidateTextAndDateTimes(value);
            ShouldBeIdEmpty(value.Id, false);
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessage());
            var result = await _itemUpdaterService.UpdateItemAsync(value);
            if (!result.WasFound)
                return NotFound();

            return Ok(result.Item);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteItemAsync(Guid id)
        {
            ShouldBeIdEmpty(id, false);
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessage());
            var retrievedItem = await _itemGetterService.GetItemAsync(id);
            if (!retrievedItem.WasFound)
                return NotFound();
            await _repository.DeleteItemAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private string GetErrorMessage() =>
            string.Join(" & ", ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage));

        private void ValidateTextAndDateTimes(Item item)
        {
            ValidateText(item.Text);
            ValidateLastUpdateTime(item.LastUpdateTime);
            ValidateCreationTime(item.CreationTime);
        }

        private void ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
                ModelState.AddModelError("ItemError", "Text cannot be empty.");
        }

        private void ValidateCreationTime(DateTime time)
        {
            if (time != DateTime.MinValue)
                ModelState.AddModelError("ItemError", "CreationTime cannot be set.");
        }

        private void ValidateLastUpdateTime(DateTime time)
        {
            if (time != DateTime.MinValue)
                ModelState.AddModelError("ItemError", "LastUpdateTime cannot be set.");
        }

        private void ShouldBeIdEmpty(Guid id, bool shouldBeEmpty)
        {
            if (id == Guid.Empty != shouldBeEmpty)
                ModelState.AddModelError("ItemError", shouldBeEmpty ? "Id must be empty." : "Id cannot be empty.");
        }
    }
}