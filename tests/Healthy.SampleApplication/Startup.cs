using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var resisConnectionString = "";
            var sqlConnectionString = "";
            var sqlQuery = "";

            app.UseHealthy(cfg =>
                cfg.ConfigureTests(tests =>
            {
                tests.AddRedisTest("name of test", resisConnectionString);
                // tests.AddRedisTest("name of test", resisConnectionString, redisTestConfiguration => { });

                // tests.AddSqlServerTest("name of test", sqlConnectionString);
                // tests.AddSqlServerTest("Name of test", sqlConnectionString, sqlQuery);

                // tests.AddFileSystemTest("name of test", fileSystemTestCOnfiguration => { });

                // tests.AddWebTest("name of test", "url"); //GET
                // tests.AddWebTest("name of test", "url", "POST", "BODY"); //POST
            })
            .ConfigureOutputs(o =>
            {
                o.AddHttpPanel("/path/to/output"); // good looking page with statistics
                o.AddHealthCheckUrl("/_health"); // enpoint that can be monitored by haproxy
                //o.AddWebHook("uri", Formatters.Json); // periodically run tests 
                o.AddHeartBeat("url", 15, "GET", true); // ping page if all tests pass
            }));

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
