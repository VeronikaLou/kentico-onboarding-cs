using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using KenticoOnboardingApplication.Api.Controllers;
using KenticoOnboardingApplication.Contracts.Helpers;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Repositories;
using KenticoOnboardingApplication.Tests.Base;
using KenticoOnboardingApplication.Tests.Base.Factories;
using NSubstitute;
using NUnit.Framework;
using Unity;

namespace KenticoOnboardingApplication.Api.Tests.Controllers
{
    [TestFixture]
    public class ListControllerIntegrationTest
    {
        [Test]
        public async Task PutItemAsync_ShouldCallRepositoryOnce()
        {
            var container = CreateContainer();
            var controller = ResolveController(container);
            var repository = container.Resolve<IListRepository>();
            var timeManager = container.Resolve<ITimeManager>();
            var (originalItem, expectedItem, itemToPut) = CreateOriginalExpectedAndItemToPut();
            ConfigureRepositoryAndTimeManager(repository, timeManager, originalItem, expectedItem);

            var result = await controller.PutItemAsync(itemToPut.Id, itemToPut);
            var executedResult = await result.ExecuteAsync(CancellationToken.None);
            executedResult.TryGetContentValue(out Item resultItem);

            await CheckRepositoryCalls(repository, originalItem);
            timeManager.Received(1).GetCurrentTime();
            Assert.That(resultItem, Is.EqualTo(expectedItem).UsingItemComparer());
            Assert.That(executedResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        private static void ConfigureRepositoryAndTimeManager(IListRepository repository, ITimeManager timeManager,
            Item originalItem, Item expectedItem)
        {
            repository.UpdateItemAsync(Arg.Is<Item>(item => item.Id == originalItem.Id))
                .Returns(result => Task.FromResult((Item) result[0]));
            repository.GetItemAsync(originalItem.Id).Returns(Task.FromResult(originalItem));
            timeManager.GetCurrentTime().Returns(expectedItem.LastUpdateTime);
        }

        private static (Item originalItem, Item expectedItem, Item itemToPut) CreateOriginalExpectedAndItemToPut()
        {
            const string lastUpdateTime = "2018-12-24";
            const string id = "00000000-0000-0000-0000-000000000005";
            var originalItem = ItemsCreator.CreateItem(id: id, text: "text");
            var itemToPut = ItemsCreator.CreateItem(id, "new text");
            var expectedItem = ItemsCreator.CreateItem(id, itemToPut.Text, lastUpdateTime: lastUpdateTime);

            return (originalItem, expectedItem, itemToPut);
        }

        private static ListController ResolveController(IUnityContainer container)
        {
            var controller = container.Resolve<ListController>();
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage();

            return controller;
        }

        private static IUnityContainer CreateContainer()
        {
            IUnityContainer container = new UnityContainer();
            DependencyConfig.RegisterDependencies(container);
            container
                .RegisterInstance(new HttpRequestMessage())
                .RegisterInstance(Substitute.For<ITimeManager>())
                .RegisterInstance(Substitute.For<IListRepository>());

            return container;
        }

        private static async Task CheckRepositoryCalls(IListRepository repository, Item originalItem)
        {
            await repository.Received(1).GetItemAsync(originalItem.Id);
            await repository.DidNotReceive().GetItemAsync(Arg.Is<Guid>(id => id != originalItem.Id));
            await repository.Received(1).UpdateItemAsync(Arg.Is<Item>(item => item.Id == originalItem.Id));
            await repository.DidNotReceive().UpdateItemAsync(Arg.Is<Item>(item => item.Id != originalItem.Id));
            await repository.DidNotReceive().GetAllItemsAsync();
            await repository.DidNotReceive().DeleteItemAsync(Arg.Any<Guid>());
            await repository.DidNotReceive().AddItemAsync(Arg.Any<Item>());
        }
    }
}