using Healthy.Core.Engine.HealthChecks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthy.Core
{
    public interface IHeatlCheckResultStorage
    {
        void Save(IObservable<HealthCheckResult> restResults);
    }
}