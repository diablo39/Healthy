using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine.HealthChecks
{

    public abstract class HealthCheckResultProcessor
    {
        public abstract void Process(HealthCheckResult result);
    }
}
