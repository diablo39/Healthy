using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Healthy.Core.Engine.HealthChecks;

namespace Healthy.Core.Engine.Storage
{
    public class InMemoryStorage : IHealthCheckResultStorage
    {
        private static ConcurrentDictionary<string, HealthCheckResult> _results = new ConcurrentDictionary<string, HealthCheckResult>();

        public Task<HealthCheckResult> GetLastResultAsync(string healthCheckId)
        {
            HealthCheckResult result;
            if (_results.TryGetValue(healthCheckId, out result))
                return Task.FromResult( result);

            return Task.FromResult< HealthCheckResult>(HealthCheckResult.Empty);
        }

        public Task SaveAsync(HealthCheckResult result)
        {
            _results.AddOrUpdate(result.HealthCheckId, result, (key, value) => result);
            return Task.CompletedTask;
        }
    }
}
