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

        public static ConcurrentBag<HealthCheckController> HealthCheckControllers = new ConcurrentBag<HealthCheckController>();

        private bool _isRunning = false;

        private readonly ILoggerFactory _loggerFactory;

        public HealthCheckService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public HealthCheckController AddHealthCheck(IHealthCheck healthCheck)
        {
            var healthCheckController = new HealthCheckController(healthCheck, _defaultHealthCheckInterval, _loggerFactory.CreateLogger<HealthCheckController>());
            HealthCheckControllers.Add(healthCheckController);

            if (_isRunning)
            {
                healthCheckController.Start();
            }

            return healthCheckController;
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

            foreach (var healthCheckRunner in HealthCheckControllers)
            {
                healthCheckRunner.Start();
            }
        }

        public void Stop()
        {
            if (!_isRunning) return;

            _isRunning = false;

            foreach (var healthCheckRunner in HealthCheckControllers)
            {
                healthCheckRunner.Stop();
            }
        }

        public void Dispose()
        {
            _isRunning = false;
            HealthCheckController healthCheckRunner = null;
            while (HealthCheckControllers.TryTake(out healthCheckRunner))
            {
                healthCheckRunner.Dispose();
            }
        }
    }
}
