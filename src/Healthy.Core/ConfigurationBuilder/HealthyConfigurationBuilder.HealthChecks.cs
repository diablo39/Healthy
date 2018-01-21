using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.HealthChecks;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder
    {
        private HealthCheckService _healthChecksService;

        public IHealthCheckConfigurator AddHealthCheck(IHealthCheck healthCheck)
        {
            var healthCheckRunnerService = _healthChecksService;
            var healthCheckRunner = healthCheckRunnerService.AddHealthCheck(healthCheck);
            var configurator = new HealthCheckConfigurator(healthCheckRunner);
            return configurator;
        }

        public void SetDefaultHealthCheckInterval(TimeSpan timeSpan)
        {
            var healthChecksRunnerService = _healthChecksService;
            healthChecksRunnerService.SetDefaultHealthCheckInterval(timeSpan);
        }
    }
}