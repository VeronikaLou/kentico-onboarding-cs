using System;

namespace KenticoOnboardingApplication.Contracts.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public override string ToString() => $"Item {Id} = {Text}";
    }
}