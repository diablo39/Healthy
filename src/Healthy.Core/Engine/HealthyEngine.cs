using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using System.Linq;
using Healthy.Core.Engine.HealthChecks;
using Healthy.Core.Engine.Storage;

namespace Healthy.Core.Engine
{
    internal partial class HealthyEngine : IHealthyEngine, IService
    {
        private readonly ILogger<HealthyEngine> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly HealthCheckService _healthCheckService;

        private ConcurrentBag<IService> _services = new ConcurrentBag<IService>();

        public IHealthCheckEngine HealthCheckEngine => _healthCheckService;

        public IHealthCheckResultStorage HealthCheckResultStorage { get; }

        public HealthyEngine(ILoggerFactory loggerFactory, HealthCheckService healthCheckService, IHealthCheckResultStorage healthCheckResultStorage)
        {
            _loggerFactory = loggerFactory;
            _healthCheckService = healthCheckService;
            _logger = loggerFactory.CreateLogger<HealthyEngine>();
            HealthCheckResultStorage = healthCheckResultStorage;
        }

        public void Start()
        {
            foreach (var service in _services)
            {
                service.Start();
            }
        }

        public void Stop()
        {
            foreach (var service in _services)
            {
                service.Stop();
            }
        }

        public T GetService<T>()
            where T : IService
        {
            var result = _services.OfType<T>().FirstOrDefault();
            return result;
        }

        internal void RegisterService(IService service)
        {
            _services.Add(service);
        }
    }
}