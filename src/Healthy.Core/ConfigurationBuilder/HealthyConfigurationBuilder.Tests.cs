using System;
using Microsoft.AspNetCore.Builder;
using Healthy.Core.Engine;
using Healthy.Core.Engine.Tests;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder
    {
        Lazy<TestsRunner> _testRunner;

        public void AddTest(string testName, ITest test)
        {
            var testRunner = _testRunner.Value;
            testRunner.AddTest(testName, test);
        }
    }
}