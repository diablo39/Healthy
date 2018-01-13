using System;
using Microsoft.AspNetCore.Builder;
using Healthy.Core.Engine;

namespace Healthy.Core.ConfigurationBuilder
{
    internal class HealthyConfigurationBuilder: IHealthyConfigurationBuilder, IOutputConfigurationBuilder, ITestsConfigurationBuilder // split into interfaces
    {
        private readonly HealthyEngine _engine;

        public HealthyEngine Engine => _engine;

        public HealthyConfigurationBuilder(HealthyEngine engine)
        {
            _engine = engine;
        }

        public IHealthyConfigurationBuilder ConfigureTests(Action<ITestsConfigurationBuilder> builder)
        {
            builder(this);
            return this;
        }

        public IHealthyConfigurationBuilder ConfigureOutputs(Action<IOutputConfigurationBuilder> builder)
        {
            builder(this);
            return this;
        }

        public void AddTest(string testName, ITest test)
        {
            _engine.AddTest(testName, test);
        }

        public void AddHttpPanel(string path)
        {
            throw new NotImplementedException();
        }

        public void AddHealthCheckUrl(string path)
        {
            throw new NotImplementedException();
        }

        public void AddHeartBeat(string url, int interval, string method, bool sendWhenTestFails = false)
        {
            throw new NotImplementedException();
        }
    }
}