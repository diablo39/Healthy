using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.Outputs.HttpPanel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;


namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder
    {
        public void AddHttpPanel(string path)
        {
            _appBuilder.Map(path, app =>
            {
                app.UseMiddleware<HttpPanelMiddleware>();
                app.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Hello from output!");
                });
            });
        }

        public void AddHealthCheckUrl(string path)
        {
            throw new NotImplementedException();
        }
    }
}