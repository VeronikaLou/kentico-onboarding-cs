namespace KenticoOnboardingApplication.Contracts.Models
{
    public class RetrievedItem
    {
        public Item Item { get; set; }
        public bool WasFound => Item != null;

        public RetrievedItem(Item item) => Item = item;
    }
}