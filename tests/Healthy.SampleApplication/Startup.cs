using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reactive.Linq;
using Healthy.Core;

namespace Healthy.SampleApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services

            services.AddHealthy();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(true); - added by default
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //var resisConnectionString = "";
            var sqlConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Owl;Integrated Security=True";
            var sqlQuery = "select 1";

            app.UseHealthy(cfg => // set backend for healthy, allow custom repository. InMemory stores only last execution
                cfg.ConfigureHealthChecks(checks =>
                {
                    checks.SetDefaultHealthCheckInterval(TimeSpan.FromSeconds(15)); // by default run each health check every 15 seconds

                    checks.AddSqlServerHealthCheck("name of sql health check", sqlConnectionString)
                            .RunEvery(TimeSpan.FromSeconds(10));

                    checks.AddSqlServerHealthCheck("Name of sql health check with query", sqlConnectionString, sqlQuery)
                            .RunEvery(7);

                })
                .ConfigureOutputs(o =>
                {
                    o.AddHealthCheckUrl("/_health"); // enpoint that can be monitored by haproxy, checks 
                })
            );

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
