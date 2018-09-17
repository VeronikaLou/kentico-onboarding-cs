using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using KenticoOnboardingApplication.Api.Models;

namespace KenticoOnboardingApplication.Api.Controllers
{
    public class ListController : ApiController
    {
        private static readonly Item[] Items =
        {
            new Item {Text = "Learn C#"},
            new Item {Text = "Create dummy controller"},
            new Item {Text = "Connect JS and TS"}
        };

        public async Task<IHttpActionResult> GetAllItems() =>
            await Task.FromResult(Ok(Items));

        public async Task<IHttpActionResult> GetItem(Guid id) =>
            await Task.FromResult(Ok(Items[0]));

        public async Task<IHttpActionResult> PostItem([FromBody] Item value)
        {
            var guid = new {id = "d95f4249-6f37-46ab-b102-b55972306910"};
            var url = new UrlHelper(Request).Route(WebApiConfig.RouteName, guid);
            var uri = new Uri(url, UriKind.Relative);
            return await Task.FromResult(Created(uri, Items[1]));
        }

        public async Task<IHttpActionResult> PutItem(Guid id, [FromBody] Item value) =>
            await Task.FromResult(Ok(Items[0]));

        public async Task<IHttpActionResult> DeleteItem(Guid id) =>
            await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
    }
}