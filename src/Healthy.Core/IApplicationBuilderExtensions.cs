using System;
using Healthy.Core.ConfigurationBuilder;
using Healthy.Core.Engine;
using Healthy.Core.Engine.HealthChecks;
using Microsoft.AspNetCore.Builder;

namespace Healthy.Core
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseHealthy(this IApplicationBuilder app, Action<IHealthyConfigurationBuilder> cfg)
        {
            var healthyEngine = (HealthyEngine)app.ApplicationServices.GetService(typeof(IHealthyEngine));

            healthyEngine.RegisterService((HealthCheckService)app.ApplicationServices.GetService(typeof(HealthCheckService)));

            var configurationBuilder = new HealthyConfigurationBuilder(healthyEngine, app);

            cfg(configurationBuilder);

            healthyEngine.Start();
        }
    }
}
