using System;
using System.Collections.Generic;
using System.Text;
using Healthy.Core.Engine.HealthChecks;

namespace Healthy.Core.ConfigurationBuilder
{
    class HealthCheckConfigurator : IHealthCheckConfigurator
    {
        private HealthCheckController _healthCheckcontroller;

        public HealthCheckConfigurator(HealthCheckController healthCheckController)
        {
            _healthCheckcontroller = healthCheckController;
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
            _healthCheckcontroller.SetHealthCheckInterval(interval);
            return this;
        }

        public IHealthCheckConfigurator RunEvery(int seconds)
        {
            _healthCheckcontroller.SetHealthCheckInterval(TimeSpan.FromSeconds(seconds));
            return this;
        }

        public IDisposable Subscribe(IObserver<HealthCheckResult> observer)
        {
            return _healthCheckcontroller.Subscribe(observer);
        }
    }
}
