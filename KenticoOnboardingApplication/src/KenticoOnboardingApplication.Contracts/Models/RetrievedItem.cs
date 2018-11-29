namespace KenticoOnboardingApplication.Contracts.Models
{
    public class RetrievedItem<TItem> where TItem: class 
    {
        public TItem Item { get; }
        public bool WasFound => Item != null;
        public static RetrievedItem<TItem> Null = new RetrievedItem<TItem>(null);

        public RetrievedItem(TItem item) => Item = item;
    }
}