using Healthy.Core.ConfigurationBuilder;
using Healthy.Core.Engine.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.HealthChecks
{
    class HealthCheckService : IService, IHealthCheckEngine
    {
        private TimeSpan _defaultHealthCheckInterval = TimeSpan.FromSeconds(15);

        private readonly ConcurrentBag<HealthCheckController> HealthCheckControllers = new ConcurrentBag<HealthCheckController>();

        private bool _isRunning = false;

        private readonly ILoggerFactory _loggerFactory;
        private readonly IHealthCheckResultStorage _resultStorage;
        private StorageObserver _resultStorageObserver;

        public IEnumerable<IHealthCheck> HealthChecks => HealthCheckControllers.Select(e=> e.HealthCheck);

        public HealthCheckService(ILoggerFactory loggerFactory, IHealthCheckResultStorage resultStorage)
        {
            _loggerFactory = loggerFactory;
            _resultStorage = resultStorage;
            _resultStorageObserver = new StorageObserver(_resultStorage);
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

        public void Add(IHealthCheck healthCheck)
        {
            Add(healthCheck, null);
        }

        public void Add(IHealthCheck healthCheck, Action<IHealthCheckConfigurator> configurator)
        {
            var healthCheckController = new HealthCheckController(healthCheck, _defaultHealthCheckInterval, _loggerFactory.CreateLogger<HealthCheckController>());
            HealthCheckControllers.Add(healthCheckController);

            healthCheckController.Subscribe(_resultStorageObserver);

            if(configurator != null)
            {
                var c = new HealthCheckConfigurator(healthCheckController);
                configurator(c);
            }

            if (_isRunning)
            {
                healthCheckController.Start();
            }
        }
    }
}
