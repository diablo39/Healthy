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

            //var resisConnectionString = "";
            var sqlConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Owl;Integrated Security=True";
            var sqlQuery = "select 1";

            app.UseHealthy(cfg => // set backend for healthy, allow custom repository. InMemory stores only last execution
                cfg.ConfigureTests(tests =>
            {
                //tests.SetDefaultScheduler(TimeSpan.FromSeconds(15)); // run each test every 15 seconds
                //tests.AddRedisTest("name of test", resisConnectionString)/*.RunEvery(TimeSpan | int) seconds*/;
                //tests.AddRedisTest("name of test", resisConnectionString, redisTestConfiguration => { });
                //tests.AddRedisTest("name of test", resisConnectionString, new RedisTest()); // custom redis tester. Have access to StaskExchange.Redis

                tests.AddSqlServerTest("name of sql test", sqlConnectionString);
                tests.AddSqlServerTest("Name of sql test with query", sqlConnectionString, sqlQuery);

                //tests.AddFileSystemTest("name of test", fileSystemTestConfiguration => { });

                // tests.AddWebTest("name of test", "url"); //GET
                // tests.AddWebTest("name of test", "url", "POST", "BODY"); //POST
            })
            .ConfigureOutputs(o =>
            {
                //o.AddHttpPanel("/path/to/output"); // good looking page with statistics
                //o.AddHealthCheckUrl("/_health"); // enpoint that can be monitored by haproxy, checks 
                //o.AddWebHook("uri", Formatters.Json, TestStatus); // web hook after test run 
                //o.AddHeartBeat("url", 15, "GET", true); // ping page if all tests pass
            })); // add monitors - based on http responses, windows counters, etc...

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
