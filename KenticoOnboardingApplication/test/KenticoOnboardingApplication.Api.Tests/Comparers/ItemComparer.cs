using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace KenticoOnboardingApplication.Api.Tests.Comparers
{
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

                return first.Id == second.Id && first.Text == second.Text;
            }

            public int GetHashCode(Item item) => item.Id.GetHashCode() + item.Text.GetHashCode();
        }

        public static EqualConstraint UsingItemComparer(this EqualConstraint constraint) =>
            constraint.Using(LazyComparer.Value);
    }
}