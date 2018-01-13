using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace Healthy.Core
{
    public static class IServiceCollectionExtensions
    {
        public static void AddHealthy(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<TestResultProcessor, TestResultProcessorAggregator>();
            serviceCollection.AddTransient<TestsRunner>();
            serviceCollection.AddSingleton<HealthyEngine>();
        }
    }
}