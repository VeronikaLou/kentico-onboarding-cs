using System.Configuration;
using KenticoOnboardingApplication.Contracts.Repositories;

namespace KenticoOnboardingApplication.Api.Repositories
{
    internal class ConnectionStrings : IConnectionStrings
    {
        string IConnectionStrings.Mongo =>
            ConfigurationManager.ConnectionStrings["mongo"].ConnectionString;
    }
}