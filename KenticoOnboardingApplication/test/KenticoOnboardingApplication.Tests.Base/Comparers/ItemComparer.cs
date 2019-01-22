using System;
using System.Collections.Generic;
using KenticoOnboardingApplication.Contracts.Models;

namespace KenticoOnboardingApplication.Tests.Base.Comparers
{
    public sealed class ItemComparer : IEqualityComparer<Item>
    {
        private static readonly Lazy<ItemComparer> s_lazyComparer = new Lazy<ItemComparer>(() => new ItemComparer());
        public static ItemComparer LazyComparer => s_lazyComparer.Value;

        private ItemComparer() {}

        public bool Equals(Item first, Item second)
        {
            if (first == null && second == null)
            {
                return true;
            }

            if (first == null || second == null)
            {
                return false;
            }

            return first.Id == second.Id &&
                   first.Text == second.Text &&
                   first.CreationTime == second.CreationTime &&
                   first.LastUpdateTime == second.LastUpdateTime;
        }

        public int GetHashCode(Item item) =>
            item.Id.GetHashCode() +
            item.Text.GetHashCode() +
            item.CreationTime.GetHashCode() +
            item.LastUpdateTime.GetHashCode();
        }
}