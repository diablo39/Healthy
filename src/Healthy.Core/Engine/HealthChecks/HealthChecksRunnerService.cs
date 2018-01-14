using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.HealthChecks
{
    class HealthCheckService : IService
    {
        private TimeSpan _defaultHealthCheckInterval = TimeSpan.FromSeconds(15);

        private ConcurrentBag<HealthCheckController> _healthCheckRunners = new ConcurrentBag<HealthCheckController>();

        private bool _isRunning = false;

        private readonly ILoggerFactory _loggerFactory;

        public HealthCheckService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public HealthCheckController AddHealthCheck(IHealthCheck healthCheck)
        {
            var healthCheckRunner = new HealthCheckController(healthCheck, _defaultHealthCheckInterval, _loggerFactory.CreateLogger<HealthCheckController>());
            _healthCheckRunners.Add(healthCheckRunner);

            if (_isRunning)
            {
                healthCheckRunner.Start();
            }

            return healthCheckRunner;
        }

        public void SetDefaultHealthCheckInterval(int interval)
        {
            SetDefaultHealthCheckInterval(TimeSpan.FromSeconds(interval));
        }

        public void SetDefaultHealthCheckInterval(TimeSpan interval)
        {
            _defaultHealthCheckInterval = interval;
        }

        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;

            foreach (var healthCheckRunner in _healthCheckRunners)
            {
                healthCheckRunner.Start();
            }
        }

        public void Stop()
        {
            if (!_isRunning) return;

            _isRunning = false;

            foreach (var healthCheckRunner in _healthCheckRunners)
            {
                healthCheckRunner.Stop();
            }
        }

        public void Dispose()
        {
            _isRunning = false;
            HealthCheckController healthCheckRunner = null;
            while (_healthCheckRunners.TryTake(out healthCheckRunner))
            {
                healthCheckRunner.Dispose();
            }
        }
    }
}
