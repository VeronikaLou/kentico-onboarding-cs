using System;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Services.Wrappers;

namespace KenticoOnboardingApplication.Tests.Base.Factories
{
    public class ItemsCreator
    {
        public static Item CreateItem(string id = null, string text = null, string creationTime = null,
            string lastUpdateTime = null)
        {
            var item = new Item();

            if (id != null)
            {
                item.Id = new Guid(id);
            }

            if (text != null)
            {
                item.Text = text;
            }

            if (creationTime != null)
            {
                item.CreationTime = DateTime.Parse(creationTime);
            }

            if (lastUpdateTime != null)
            {
                item.LastUpdateTime = DateTime.Parse(lastUpdateTime);
            }

            return item;
        }

        public static (Item item, RetrievedItem<Item> retrievedItem) CreateItemAndRetrievedItem(string id = null,
            string text = null, string creationTime = null, string lastUpdateTime = null)
        {
            var item = CreateItem(id, text, creationTime, lastUpdateTime);

            return (item, new RetrievedItem<Item>(item));
        }
    }
}