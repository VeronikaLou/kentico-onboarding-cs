using System;

namespace KenticoOnboardingApplication.Api.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"Item {Id} = {Text}";
        }
    }
}