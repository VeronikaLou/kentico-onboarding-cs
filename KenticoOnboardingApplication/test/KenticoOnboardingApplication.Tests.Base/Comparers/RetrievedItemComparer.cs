using System;
using System.Collections.Generic;
using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Contracts.Services.Wrappers;

namespace KenticoOnboardingApplication.Tests.Base.Comparers
{
    public sealed class RetrievedItemComparer : IEqualityComparer<RetrievedItem<Item>>
    {
        private static readonly Lazy<RetrievedItemComparer> s_lazyComparer = new Lazy<RetrievedItemComparer>(() => new RetrievedItemComparer());
        public static RetrievedItemComparer LazyComparer => s_lazyComparer.Value;

        private RetrievedItemComparer() {}

        public bool Equals(RetrievedItem<Item> first, RetrievedItem<Item> second)
        {
            if (first == null && second == null)
            {
                return true;
            }

            if (first == null || second == null)
            {
                return false;
            }

            return ItemComparer.LazyComparer.Equals(first.Item, second.Item) && first.WasFound == second.WasFound;
        }

        public int GetHashCode(RetrievedItem<Item> retrievedItem) =>
            ItemComparer.LazyComparer.GetHashCode(retrievedItem.Item) + retrievedItem.WasFound.GetHashCode();
    }
}