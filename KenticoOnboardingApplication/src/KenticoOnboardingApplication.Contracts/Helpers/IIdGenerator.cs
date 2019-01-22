namespace KenticoOnboardingApplication.Contracts.Helpers
{
    public interface IIdGenerator<out TId>
    {
        TId GenerateId();
    }
}