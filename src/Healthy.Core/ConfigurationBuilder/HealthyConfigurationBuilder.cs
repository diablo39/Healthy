using Healthy.Core.Engine;
using Healthy.Core.Engine.HealthChecks;
using Healthy.Core.Engine.Outputs.HttpPanel;
using Microsoft.AspNetCore.Builder;
using System;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder :
        IHealthyConfigurationBuilder, IOutputConfigurationBuilder, IHealthChecksConfigurationBuilder // split into interfaces
    {
        private readonly IApplicationBuilder _appBuilder;
        private readonly HealthyEngine _engine;
        private HealthCheckService _healthChecksService;

        public HealthyConfigurationBuilder(HealthyEngine engine, IApplicationBuilder appBuilder)
        {
            _engine = engine;
            this._appBuilder = appBuilder;
            _healthChecksService = _engine.GetService<HealthCheckService>();
        }

        public void AddHealthCheck(IHealthCheck healthCheck)
        {
            _healthChecksService.Add(healthCheck);
        }

        public void AddHealthCheck(IHealthCheck healthCheck, Action<IHealthCheckConfigurator> configurator = null)
        {
            _healthChecksService.Add(healthCheck, configurator);
        }

        public void AddHealthCheckUrl(string path)
        {
            throw new NotImplementedException();
        }

        public void AddHttpPanel(string path)
        {
            _appBuilder.Map(path, app =>
            {
                app.UseMiddleware<HttpPanelMiddleware>();
            });
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
        public void SetDefaultHealthCheckInterval(TimeSpan timeSpan)
        {
            _healthChecksService.SetDefaultHealthCheckInterval(timeSpan);
        }
    }
}