using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.HealthChecks;
using Microsoft.AspNetCore.Builder;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder: 
        IHealthyConfigurationBuilder, IOutputConfigurationBuilder, IHealthChecksConfigurationBuilder // split into interfaces
    {
        private readonly HealthyEngine _engine;
        private readonly IApplicationBuilder _appBuilder;

        public HealthyConfigurationBuilder(HealthyEngine engine, IApplicationBuilder appBuilder)
        {
            _engine = engine;
            this._appBuilder = appBuilder;
            _healthChecksService = _engine.GetService<HealthCheckService>();
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