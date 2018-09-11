using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using KenticoOnboardingApplication.Api.Controllers;
using NUnit.Framework;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace KenticoOnboardingApplication.Api.Tests
{
    [TestFixture]
    public class ListControllerTest
    {
        private ListController controller;

        [SetUp]
        public void SetUp()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("TestApi", "testTemplate");

            controller = new ListController()
            {
                Configuration = config,
                Request = new System.Net.Http.HttpRequestMessage()
            };
        }

        [Test]
        public async Task GetAllItems()
        {
            var expectedResult = controller.items;

            var result = await controller.Get();
            var executedResult = await result.ExecuteAsync(CancellationToken.None);
            string[] resultValue;
            executedResult.TryGetContentValue(out resultValue);

            Assert.AreEqual(executedResult.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(resultValue, expectedResult);
        }
    }
}
