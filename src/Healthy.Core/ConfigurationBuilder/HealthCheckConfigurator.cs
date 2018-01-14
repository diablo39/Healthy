using System;
using System.Collections.Generic;
using System.Text;
using Healthy.Core.Engine.HealthChecks;

namespace Healthy.Core.ConfigurationBuilder
{
    class HealthCheckConfigurator : IHealthCheckConfigurator
    {
        private HealthCheckRunner _healthCheckRunner;

        public HealthCheckConfigurator(HealthCheckRunner healthCheckRunner)
        {
            _healthCheckRunner = healthCheckRunner;
        }

        public IHealthCheckConfigurator AddTag(string tag)
        {
            throw new NotImplementedException();
        }

        public IHealthCheckConfigurator AddTags(params string[] tags)
        {
            throw new NotImplementedException();
        }

        public IHealthCheckConfigurator RunEvery(TimeSpan interval)
        {
            _healthCheckRunner.SetHealthCheckInterval(interval);
            return this;
        }

        public IHealthCheckConfigurator RunEvery(int seconds)
        {
            _healthCheckRunner.SetHealthCheckInterval(TimeSpan.FromSeconds(seconds));
            return this;
        } 
    }
}
