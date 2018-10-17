using System.Configuration;
using KenticoOnboardingApplication.Contracts.Repositories;

namespace KenticoOnboardingApplication.Api.Repositories
{
    internal class ConnectionString : IConnectionString
    {
        string IConnectionString.TodoList =>
            ConfigurationManager.ConnectionStrings["TODOList"].ConnectionString;
    }
}