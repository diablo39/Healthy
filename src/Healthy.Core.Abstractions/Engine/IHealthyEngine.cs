using Healthy.Core.Engine.HealthChecks;
using Healthy.Core.Engine.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine
{
    public interface IHealthyEngine: IService
    {
        IHealthCheckEngine HealthCheckEngine { get; }

        IHealthCheckResultStorage HealthCheckResultStorage { get; }
    }
}
