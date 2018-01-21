using Healthy.Core.Engine.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine.Storage
{
    public interface IHealthCheckResultStorage
    {
        void SaveAsync(HealthCheckResult result);
    }
}
