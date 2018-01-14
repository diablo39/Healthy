using Healthy.Core.Engine.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.ConfigurationBuilder
{
    public interface IHealthCheckConfigurator: IObservable<HealthCheckResult>
    {
        IHealthCheckConfigurator RunEvery(TimeSpan interval);

        IHealthCheckConfigurator RunEvery(int seconds);

        IHealthCheckConfigurator AddTag(string tag);

        IHealthCheckConfigurator AddTags(params string[] tags);
    }
}
