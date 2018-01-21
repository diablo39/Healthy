using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using System.Linq;
using Healthy.Core.Engine.HealthChecks;

namespace Healthy.Core.Engine
{
    internal partial class HealthyEngine : IHealthyEngine, IService
    {
        private readonly ILogger<HealthyEngine> _logger;
        private readonly ILoggerFactory _loggerFactory;

        private ConcurrentBag<IService> _services = new ConcurrentBag<IService>();

        // List of storages

        public HealthyEngine(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<HealthyEngine>();
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

        public void RegisterService(IService service)
        {
            _services.Add(service);
        }
    }
}