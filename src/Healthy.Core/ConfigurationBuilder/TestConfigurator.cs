using System;
using System.Collections.Generic;
using System.Text;
using Healthy.Core.Engine.Tests;

namespace Healthy.Core.ConfigurationBuilder
{
    class TestConfigurator : ITestConfigurator
    {
        private TestRunner _testRunner;

        public TestConfigurator(TestRunner testRunner)
        {
            _testRunner = testRunner;
        }

        public ITestConfigurator AddTag(string tag)
        {
            throw new NotImplementedException();
        }

        public ITestConfigurator AddTags(params string[] tags)
        {
            throw new NotImplementedException();
        }

        public ITestConfigurator RunEvery(TimeSpan interval)
        {
            _testRunner.SetTestingInterval(interval);
            return this;
        }

        public ITestConfigurator RunEvery(int seconds)
        {
            _testRunner.SetTestingInterval(TimeSpan.FromSeconds(seconds));
            return this;
        }

        
    }
}
