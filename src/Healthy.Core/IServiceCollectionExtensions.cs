using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.HealthChecks;
using Healthy.Core.Engine.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Healthy.Core
{
    public static class IServiceCollectionExtensions
    {
        public static void AddHealthy(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<HealthCheckService>();

            serviceCollection.AddSingleton<HealthCheckService>();
            serviceCollection.AddTransient<IHealthCheckResultStorage, InMemoryStorage>();

            serviceCollection.AddSingleton<IHealthyEngine, HealthyEngine>();
        }
    }
}