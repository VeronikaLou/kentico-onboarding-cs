using Unity;

namespace KenticoOnboardingApplication.Contracts
{
    public interface IBootstrapper
    {
        IUnityContainer Register(IUnityContainer container);
    }
}