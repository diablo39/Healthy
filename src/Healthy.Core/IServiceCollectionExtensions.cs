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
            serviceCollection.AddTransient<HealthCheckService>();
            serviceCollection.AddSingleton<HealthyEngine>();
        }
    }
}