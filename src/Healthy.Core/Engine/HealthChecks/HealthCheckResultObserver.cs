using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine.HealthChecks
{
    class HealthCheckResultObserver : IObserver<HealthCheckResult>, IObservable<HealthCheckResult>
    {
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
            Console.WriteLine("Observer!");
        }

        public IDisposable Subscribe(IObserver<HealthCheckResult> observer)
        {
            throw new NotImplementedException();
        }
    }
}
