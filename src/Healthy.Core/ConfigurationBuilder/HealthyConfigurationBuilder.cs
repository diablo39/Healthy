using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.Tests;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder: 
        IHealthyConfigurationBuilder, IOutputConfigurationBuilder, ITestsConfigurationBuilder, IMetricsConfigurationBuilder // split into interfaces
    {
        private readonly HealthyEngine _engine;

        public HealthyConfigurationBuilder(HealthyEngine engine)
        {
            _engine = engine;
            _testRunnerServiceAccessor = new Lazy<TestsRunnerService>(() => _engine.GetService<TestsRunnerService>());
        }

        public IHealthyConfigurationBuilder ConfigureTests(Action<ITestsConfigurationBuilder> builder)
        {
            builder(this);
            return this;
        }

        public IHealthyConfigurationBuilder ConfigureOutputs(Action<IOutputConfigurationBuilder> builder)
        {
            builder(this);
            return this;
        }

        public IHealthyConfigurationBuilder ConfigureMetrics(Action<IMetricsConfigurationBuilder> builder)
        {
            builder(this);
            return this;
        }
    }
}