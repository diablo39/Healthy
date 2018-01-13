using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine.Tests
{
    class TestResultObserver : IObserver<TestResult>, IObservable<TestResult>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Error");
        }

        public void OnNext(TestResult value)
        {
            Console.WriteLine("Observer!");
        }

        public IDisposable Subscribe(IObserver<TestResult> observer)
        {
            throw new NotImplementedException();
        }
    }
}
