using Healthy.Core.Engine.Tests;
using System;

namespace Healthy.Core.ConfigurationBuilder
{
    public interface ITestsConfigurationBuilder
    {
        ITestConfigurator AddTest(ITest test);

        void SetDefaultTestInterval(TimeSpan timeSpan);

        void AddTestResultStorage(ITestResultStorage testResultStorage);

        void RegisterTestResultProcessor(Action<TestResult> processor);
    }
}