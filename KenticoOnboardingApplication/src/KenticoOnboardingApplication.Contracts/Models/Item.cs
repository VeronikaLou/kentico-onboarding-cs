using System;

namespace KenticoOnboardingApplication.Contracts.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public DateTime Creation { get; set; } = DateTime.Now;

        public override string ToString() => $"Item {Id} = {Text}";
    }
}