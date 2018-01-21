using Healthy.Core.Engine.HealthChecks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine.Storage
{
    class StorageObserver : IObserver<HealthCheckResult>
    {
        private ConcurrentBag<IHealthCheckResultStorage> _storages;

        public void OnCompleted()
        {
            Console.WriteLine("Completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Error");
        }

        public void OnNext(HealthCheckResult value)
        {
            Console.WriteLine("On next");
        }
    }
}
