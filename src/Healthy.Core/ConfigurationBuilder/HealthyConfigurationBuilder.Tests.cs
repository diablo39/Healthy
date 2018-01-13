using System;
using Microsoft.AspNetCore.Builder;
using Healthy.Core.Engine;
using Healthy.Core.Engine.Tests;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder
    {
        public void AddTest(ITest test)
        {
            var testRunner = _testRunnerAccessor.Value;
            testRunner.AddTest(test);
        }

        public void SetDefaultTestInterval(TimeSpan timeSpan)
        {
            var testRunner = _testRunnerAccessor.Value;
            testRunner.SetDefaultTestInterval(timeSpan);
        }
    }
}