using System.Diagnostics.CodeAnalysis;
using KenticoOnboardingApplication.Tests.Base.Comparers;
using NUnit.Framework.Constraints;

namespace KenticoOnboardingApplication.Tests.Base
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    public static class ComparerWrapper
    {
        public static EqualConstraint UsingItemComparer(this EqualConstraint constraint) =>
            constraint.Using(ItemComparer.LazyComparer);

        public static EqualConstraint UsingRetrievedItemComparer(this EqualConstraint constraint) =>
            constraint.Using(RetrievedItemComparer.LazyComparer);
    }
}