using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.HealthChecks;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder
    {
        private HealthCheckService _healthChecksService;

        public void AddHealthCheck(IHealthCheck healthCheck)
        {
            _healthChecksService.Add(healthCheck);
        }

        public void AddHealthCheck(IHealthCheck healthCheck, Action<IHealthCheckConfigurator> configurator = null)
        {
            _healthChecksService.Add(healthCheck, configurator);
        }

        public void SetDefaultHealthCheckInterval(TimeSpan timeSpan)
        {
            _healthChecksService.SetDefaultHealthCheckInterval(timeSpan);
        }
    }
}