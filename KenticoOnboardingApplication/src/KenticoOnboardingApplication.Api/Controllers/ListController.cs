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
            if (!ShouldBeIdEmpty(id, false))
                return BadRequest("Invalid input format.");
            var result = await _itemGetterService.GetItemAsync(id);

            return result.WasFound ? (IHttpActionResult) Ok(result.Item) : NotFound();
        }

        public async Task<IHttpActionResult> PostItemAsync([FromBody] Item value)
        {
            if (!AreTextAndDatesCorrect(value) || !ShouldBeIdEmpty(value.Id, true))
                return BadRequest("Invalid input format.");
            var uri = _urlLocator.GetListItemUri(value.Id);
            var item = await _itemCreatorService.CreateItemAsync(value);

            return Created(uri, item);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutItemAsync(Guid id, [FromBody] Item value)
        {
            if (!AreTextAndDatesCorrect(value) || !ShouldBeIdEmpty(value.Id, false))
                return BadRequest("Invalid input format.");
            var result = await _itemUpdaterService.UpdateItemAsync(value);

            return result.WasFound ? (IHttpActionResult) Ok(result.Item) : NotFound();
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteItemAsync(Guid id)
        {
            if (!ShouldBeIdEmpty(id, false))
                return BadRequest("Invalid input format.");
            var databaseItem = await _itemGetterService.GetItemAsync(id);
            if (!databaseItem.WasFound)
                return NotFound();

            await _repository.DeleteItemAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool AreTextAndDatesCorrect(Item item)
        {
            var text = !string.IsNullOrEmpty(item.Text);
            if (!text)
                ModelState.AddModelError("ItemError", "Text cannot be empty.");
            var lastUpdate = item.LastUpdateTime == DateTime.MinValue;
            if (!lastUpdate)
                ModelState.AddModelError("ItemError", "LastUpdateTime cannot be set.");
            var creation = item.CreationTime == DateTime.MinValue;
            if (!creation)
                ModelState.AddModelError("ItemError", "CreationTime cannot be set.");
            return creation && lastUpdate && text;
        }

        private bool ShouldBeIdEmpty(Guid id, bool shouldBeEmpty)
        {
            if (id == Guid.Empty == shouldBeEmpty) return true;
            ModelState.AddModelError("ItemError", shouldBeEmpty ? "Id must be empty." : "Id cannot be empty.");
            return false;
        }
    }
}