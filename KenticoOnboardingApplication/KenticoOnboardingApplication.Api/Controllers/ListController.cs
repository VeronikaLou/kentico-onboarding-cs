using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NUnit.Framework.Constraints;

namespace KenticoOnboardingApplication.Api.Controllers
{
    public class ListController : ApiController
    {
        public readonly string[] items =
        {
            "Learn C#",
            "Create dummy controller",
            "Connect JS and TS"
        };

        // GET: api/List
        public async Task<IHttpActionResult> Get()
        {
            return await Task.FromResult<IHttpActionResult>(Ok(items));
        }

        // GET: api/List/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/List
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/List/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/List/5
        public void Delete(int id)
        {
        }
    }
}
