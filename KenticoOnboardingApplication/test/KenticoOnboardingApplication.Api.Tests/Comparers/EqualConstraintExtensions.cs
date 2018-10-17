using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using KenticoOnboardingApplication.Contracts.Models;
using NUnit.Framework.Constraints;

namespace KenticoOnboardingApplication.Api.Tests.Comparers
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    internal static class ComparerWraper
    {
        private static Lazy<ItemComparer> LazyComparer => new Lazy<ItemComparer>();

        private sealed class ItemComparer : IEqualityComparer<Item>
        {
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

                return AreItemsEqual(first, second);
            }

            public int GetHashCode(Item item) => item.Id.GetHashCode() + item.Text.GetHashCode();
        }

        public static EqualConstraint UsingItemComparer(this EqualConstraint constraint) =>
            constraint.Using(LazyComparer.Value);

        public static bool AreItemsEqual(Item first, Item second) =>
            first.Id == second.Id &&
            first.Text == second.Text &&
            first.CreationTime == second.CreationTime &&
            first.LastUpdateTime == second.LastUpdateTime;
    }
}