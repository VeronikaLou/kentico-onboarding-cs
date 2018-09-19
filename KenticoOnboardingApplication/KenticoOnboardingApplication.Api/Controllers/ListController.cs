using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Api.Models;
using Microsoft.Web.Http;

namespace KenticoOnboardingApplication.Api.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/List")]
    public class ListController : ApiController
    {
        private static readonly Item[] Items =
        {
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000001"), Text = "Learn C#"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000002"), Text = "Create dummy controller"},
            new Item {Id = new Guid("00000000-0000-0000-0000-000000000003"), Text = "Connect JS and TS"}
        };

        [Route]
        public async Task<IHttpActionResult> GetAllItems() =>
            await Task.FromResult(Ok(Items));

        [Route("{id:guid}", Name = "Get")]
        public async Task<IHttpActionResult> GetItem(Guid id) =>
            await Task.FromResult(Ok(Items[0]));

        [Route]
        public async Task<IHttpActionResult> PostItem([FromBody] Item value)
        {
            var uri = Url.Link("Get", new {id = Items[1].Id});

            return await Task.FromResult(Created(uri, Items[1]));
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutItem(Guid id, [FromBody] Item value) =>
            await Task.FromResult(Ok(Items[0]));

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteItem(Guid id) =>
            await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
    }
}