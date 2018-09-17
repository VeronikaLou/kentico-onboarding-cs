namespace KenticoOnboardingApplication.Api.Models
{
    public class Item
    {
        public string Text { get; set; }
        public bool IsEdited { get; set; } = false;

        public override string ToString()
        {
            return $"Item {Text} is edited: {IsEdited}";
        }
    }
}