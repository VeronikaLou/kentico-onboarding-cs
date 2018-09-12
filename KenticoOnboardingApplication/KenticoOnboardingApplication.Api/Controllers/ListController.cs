using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace KenticoOnboardingApplication.Api.Controllers
{
    public class ListController : ApiController
    {
        public readonly string[] Items =
        {
            "Learn C#",
            "Create dummy controller",
            "Connect JS and TS"
        };

        public async Task<IHttpActionResult> GetAllItems() => await Task.FromResult(Ok(Items));

        public async Task<IHttpActionResult> GetItem(Guid id) => await Task.FromResult(Ok(Items[0]));

        public async Task<IHttpActionResult> PostItem([FromBody] string value) => await Task.FromResult(
                Created(new Uri(new UrlHelper(Request).Route(WebApiConfig.RouteName, new { id= "d95f4249-6f37-46ab-b102-b55972306910" }), UriKind.Relative), Items[1]));

        public async Task<IHttpActionResult> PutItem(Guid id, [FromBody]string value) => await Task.FromResult(Ok(Items[0]));

        public async Task<IHttpActionResult> DeleteItem(Guid id) => await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
    }
}
