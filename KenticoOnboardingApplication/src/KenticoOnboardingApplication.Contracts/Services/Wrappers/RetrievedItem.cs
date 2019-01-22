using System;

namespace KenticoOnboardingApplication.Contracts.Services.Wrappers
{
    public class RetrievedItem<TItem> where TItem : class
    {
        private readonly TItem _item;
        public TItem Item => _item ?? throw new InvalidOperationException("Item of RetrievedItem.Empty is null.");
        public bool WasFound => _item != null;
        public static RetrievedItem<TItem> Empty { get; } = new RetrievedItem<TItem>();
        
        public RetrievedItem(TItem item) =>
            _item = item ?? throw new ArgumentNullException(nameof(item), $"Null passed to {nameof(RetrievedItem<TItem>)} constructor.");

        private RetrievedItem() { }

        public override string ToString() => WasFound? $"{Item} and was found" : "Item was not found";
    }
}