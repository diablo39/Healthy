using System;

namespace Healthy.Core.ConfigurationBuilder
{
    public interface IHealthyConfigurationBuilder
    {
        IHealthyConfigurationBuilder ConfigureHealthChecks(Action<IHealthChecksConfigurationBuilder> builder);

        IHealthyConfigurationBuilder ConfigureOutputs(Action<IOutputConfigurationBuilder> builder);
    }
}