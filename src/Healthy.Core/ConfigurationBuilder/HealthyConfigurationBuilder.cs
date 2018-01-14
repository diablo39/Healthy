using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.HealthChecks;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder: 
        IHealthyConfigurationBuilder, IOutputConfigurationBuilder, IHealthChecksConfigurationBuilder // split into interfaces
    {
        private readonly HealthyEngine _engine;

        public HealthyConfigurationBuilder(HealthyEngine engine)
        {
            _engine = engine;
            _healthChecksRunnerServiceAccessor = new Lazy<HealthCheckService>(() => _engine.GetService<HealthCheckService>());
        }

        public IHealthyConfigurationBuilder ConfigureHealthChecks(Action<IHealthChecksConfigurationBuilder> builder)
        {
            builder(this);
            return this;
        }

        public IHealthyConfigurationBuilder ConfigureOutputs(Action<IOutputConfigurationBuilder> builder)
        {
            builder(this);
            return this;
        }
    }
}