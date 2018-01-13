using System;
using Healthy.Core.Engine;
using Microsoft.Extensions.DependencyInjection;

namespace Healthy.Core
{
    public static class IServiceCollectionExtensions
    {
        public static void AddHealthy(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<HealthyEngine>();
        }
    }
}