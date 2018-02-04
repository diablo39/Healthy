using Healthy.Core.Engine.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine.Storage
{
    class StorageObserver : IObserver<HealthCheckResult>
    {
        private readonly IHealthCheckResultStorage _storage;

        public StorageObserver(IHealthCheckResultStorage storage)
        {
            _storage = storage;
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public async void OnNext(HealthCheckResult value)
        {
            try
            {
                await _storage.SaveAsync(value);
            }
            catch
            {
                // TODO: Log
            }
        }
    }
}
