using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace Healthy.Core
{
    public static class IServiceCollectionExtensions
    {
        public static void AddHealthy(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<HealthCheckResultProcessor, HealthCheckResultProcessorAggregator>();
            serviceCollection.AddTransient<HealthChecksRunnerService>();
            serviceCollection.AddSingleton<HealthyEngine>();
            serviceCollection.AddSingleton<HealthCheckResultProcessor, HealthCheckResultProcessorAggregator>();
        }
    }
}