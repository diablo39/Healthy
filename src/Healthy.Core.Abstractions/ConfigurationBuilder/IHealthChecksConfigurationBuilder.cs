using Healthy.Core.Engine.HealthChecks;
using System;

namespace Healthy.Core.ConfigurationBuilder
{
    public interface IHealthChecksConfigurationBuilder
    {
        IHealthCheckConfigurator AddHealthCheck(IHealthCheck healthCheck);

        void SetDefaultHealthCheckInterval(TimeSpan timeSpan);

        void AddHealthCheckResultStorage(IHeatlCheckResultStorage healthCheckResultStorage);

        void RegisterHealthCheckResultProcessor(Action<HealthCheckResult> processor);
    }
}