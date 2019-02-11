using KenticoOnboardingApplication.Contracts.Models;
using KenticoOnboardingApplication.Tests.Base.Comparers;
using NSubstitute;

namespace KenticoOnboardingApplication.Tests.Base
{
    public static class ArgWrapper
    {
        public static Item IsItem(Item expectedItem) =>
            Arg.Is<Item>(item => ItemComparer.LazyComparer.Equals(item, expectedItem));
    }
}