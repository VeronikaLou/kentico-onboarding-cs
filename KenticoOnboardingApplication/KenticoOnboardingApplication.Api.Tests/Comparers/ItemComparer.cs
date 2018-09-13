using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using KenticoOnboardingApplication.Api.Models;
using NUnit.Framework.Constraints;

namespace KenticoOnboardingApplication.Api.Tests.Comparers
{
    internal static class ComparerWraper
    {
        private static Lazy<ItemComparer> lazyComparer = new Lazy<ItemComparer>();

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

                return first.IsEdited == second.IsEdited && first.Text == second.Text;
            }

            public int GetHashCode(Item item) => item.IsEdited.GetHashCode() + item.Text.GetHashCode();
        }

        public static EqualConstraint UsingItemComparer(this EqualConstraint constraint) =>
            constraint.Using(lazyComparer.Value);
    }
}