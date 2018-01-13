using System;
using Microsoft.AspNetCore.Builder;

namespace Healthy.Core
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseHealthy(this IApplicationBuilder app, Action<HealthyConfigurationBuilder> cfg)
        {

        }
    }
}
