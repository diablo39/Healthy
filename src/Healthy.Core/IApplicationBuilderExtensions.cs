using System;
using Healthy.Core.ConfigurationBuilder;
using Healthy.Core.Engine;
using Healthy.Core.Engine.Tests;
using Microsoft.AspNetCore.Builder;

namespace Healthy.Core
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseHealthy(this IApplicationBuilder app, Action<IHealthyConfigurationBuilder> cfg)
        {
            var healthyEngine = (HealthyEngine)app.ApplicationServices.GetService(typeof(HealthyEngine));

            healthyEngine.RegisterService((TestsRunner)app.ApplicationServices.GetService(typeof(TestsRunner)));

            var configurationBuilder = new HealthyConfigurationBuilder(healthyEngine);

            cfg(configurationBuilder);

            healthyEngine.Start();
        }
    }
}
