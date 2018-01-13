using System;
using Microsoft.AspNetCore.Builder;

namespace Healthy.Core
{
    public class HealthyConfigurationBuilder
    {
        public HealthyConfigurationBuilder ConfigureTests(Action<TestsConfigurationBuilder> p)
        {
            return this;
        }

        public HealthyConfigurationBuilder ConfigureOutputs(Action<OutputConfigurationBuilder> b)
        {
            return this;
        }
    }

    public sealed class OutputConfigurationBuilder
    {
        private readonly IApplicationBuilder _appBuilder;

        internal OutputConfigurationBuilder(IApplicationBuilder appBuilder)
        {
            _appBuilder = appBuilder;
        }

        public void AddHttpPanel(string path)
        {
            throw new NotImplementedException(); // add httpoutput middleware
        }

        public void AddHealthCheckUrl(string path)
        {
            throw new NotImplementedException(); // add healthcheck middleware
        }

        public void AddHeartBeat(string url, int interval, string method, bool sendWhenTestFails = false)
        {
            throw new NotImplementedException();
        }
    }

    public class TestsConfigurationBuilder
    {

    }
}