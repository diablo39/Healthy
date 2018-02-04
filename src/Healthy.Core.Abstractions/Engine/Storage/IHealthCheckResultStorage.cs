using Healthy.Core.Engine.HealthChecks;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.Storage
{
    public interface IHealthCheckResultStorage
    {
        Task SaveAsync(HealthCheckResult result);

        Task<HealthCheckResult> GetLastResultAsync(string healthCheckId);
    }
}
