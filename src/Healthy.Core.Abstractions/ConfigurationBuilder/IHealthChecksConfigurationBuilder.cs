using Healthy.Core.Engine.HealthChecks;
using System;

namespace Healthy.Core.ConfigurationBuilder
{
    public interface IHealthChecksConfigurationBuilder
    {
        void AddHealthCheck(IHealthCheck healthCheck);

        void AddHealthCheck(IHealthCheck healthCheck, Action<IHealthCheckConfigurator> configurator);

        void SetDefaultHealthCheckInterval(TimeSpan timeSpan);
    }
}