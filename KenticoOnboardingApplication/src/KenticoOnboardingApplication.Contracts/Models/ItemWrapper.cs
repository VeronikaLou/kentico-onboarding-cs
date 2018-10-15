namespace KenticoOnboardingApplication.Contracts.Models
{
    public class ItemWrapper
    {
        public Item Item { get; set; }
        public bool WasFound => Item != null;

        public ItemWrapper(Item item) => Item = item;
    }
}