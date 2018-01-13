using System;

namespace Healthy.Core.ConfigurationBuilder
{
    public interface IHealthyConfigurationBuilder
    {
        IHealthyConfigurationBuilder ConfigureTests(Action<ITestsConfigurationBuilder> builder);

        IHealthyConfigurationBuilder ConfigureOutputs(Action<IOutputConfigurationBuilder> builder);
        
        IHealthyConfigurationBuilder ConfigureMetrics(Action<IMetricsConfigurationBuilder> builder);
    }
}