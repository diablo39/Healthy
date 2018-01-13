using Healthy.Core.Engine.Tests;
using System;

namespace Healthy.Core.ConfigurationBuilder
{
    public interface ITestsConfigurationBuilder
    {
        void AddTest(ITest test);
        void SetDefaultTestInterval(TimeSpan timeSpan);
    }
}