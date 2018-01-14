using System.Threading.Tasks;

namespace Healthy.Core.Engine.HealthChecks
{
    public interface IHealthCheck
    {
        string Name { get;}

        Task<HealthCheckResult> ExecuteAsync();
    }
}