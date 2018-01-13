using Healthy.Core.Engine.Tests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthy.Core
{
    public interface ITestResultStorage
    {
        void Save(IObservable<TestResult> restResults);
    }
}