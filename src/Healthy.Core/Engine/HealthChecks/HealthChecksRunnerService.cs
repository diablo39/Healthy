using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.HealthChecks
{
    class HealthChecksRunnerService : IService
    {
        private TimeSpan _defaultHealthCheckInterval = TimeSpan.FromSeconds(15);

        private ConcurrentBag<HealthCheckRunner> _healthCheckRunners = new ConcurrentBag<HealthCheckRunner>();

        private bool _isRunning = false;

        private readonly ILoggerFactory _loggerFactory;

        private readonly HealthCheckResultProcessor _healthCheckResultProcessor;

        private HealthCheckResultObserver _observer;

        internal IObservable<HealthCheckResult> HealthCheckResults { get => _observer; }

        public HealthChecksRunnerService(ILoggerFactory loggerFactory, HealthCheckResultProcessor healthCheckResultProcessor)
        {
            _loggerFactory = loggerFactory;
            _healthCheckResultProcessor = healthCheckResultProcessor;
            _observer = new HealthCheckResultObserver();
        }

        public HealthCheckRunner AddHealthCheck(IHealthCheck healthCheck)
        {
            var healthCheckRunner = new HealthCheckRunner(healthCheck, _defaultHealthCheckInterval, _loggerFactory.CreateLogger<HealthCheckRunner>());
            healthCheckRunner.Subscribe(_observer);
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
            HealthCheckRunner healthCheckRunner = null;
            while (_healthCheckRunners.TryTake(out healthCheckRunner))
            {
                healthCheckRunner.Dispose();
            }
        }
    }
}
