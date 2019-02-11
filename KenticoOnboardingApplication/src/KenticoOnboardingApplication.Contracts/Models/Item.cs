using System;

namespace KenticoOnboardingApplication.Contracts.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        
        public override string ToString() => $"Item {Id} = {Text}, was created at {CreationTime} and updated at {LastUpdateTime}";
    }
}