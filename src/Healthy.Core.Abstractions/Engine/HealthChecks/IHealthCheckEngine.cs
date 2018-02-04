using Healthy.Core.ConfigurationBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine.HealthChecks
{
    public interface IHealthCheckEngine: IService
    {
        void Add(IHealthCheck h);

        void Add(IHealthCheck h, Action<IHealthCheckConfigurator> configurator);

        IEnumerable<IHealthCheck> HealthChecks { get; }
    }
}
