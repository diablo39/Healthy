using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.HealthChecks;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder
    {
        private Lazy<HealthChecksRunnerService> _healthChecksRunnerServiceAccessor;

        public IHealthCheckConfigurator AddHealthCheck(IHealthCheck healthCheck)
        {
            var healthCheckRunnerService = _healthChecksRunnerServiceAccessor.Value;
            var healthCheckRunner = healthCheckRunnerService.AddHealthCheck(healthCheck);
            var configurator = new HealthCheckConfigurator(healthCheckRunner);
            return configurator;
        }

        public void SetDefaultHealthCheckInterval(TimeSpan timeSpan)
        {
            var healthChecksRunnerService = _healthChecksRunnerServiceAccessor.Value;
            healthChecksRunnerService.SetDefaultHealthCheckInterval(timeSpan);
        }

        public void AddHealthCheckResultStorage(IHeatlCheckResultStorage healthCheckResultStorage)
        {
            var healthCheckRunnerService = _healthChecksRunnerServiceAccessor.Value;

            healthCheckResultStorage.Save(healthCheckRunnerService.HealthCheckResults);
        }

        public void RegisterHealthCheckResultProcessor(Action<HealthCheckResult> processor)
        {
            var healthChecksRunnerService = _healthChecksRunnerServiceAccessor.Value;

            var observer = new DelegatedObserver<HealthCheckResult>(processor);

            healthChecksRunnerService.HealthCheckResults.Subscribe(observer);
        }
    }
}