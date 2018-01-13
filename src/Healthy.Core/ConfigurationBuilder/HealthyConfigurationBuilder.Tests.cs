using System;
using Healthy.Core.Engine;
using Healthy.Core.Engine.Tests;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder
    {
        private Lazy<TestsRunnerService> _testRunnerServiceAccessor;

        public ITestConfigurator AddTest(ITest test)
        {
            var testRunnerService = _testRunnerServiceAccessor.Value;
            var testRunner = testRunnerService.AddTest(test);
            var configurator = new TestConfigurator(testRunner);
            return configurator;
        }

        public void SetDefaultTestInterval(TimeSpan timeSpan)
        {
            var testRunnerService = _testRunnerServiceAccessor.Value;
            testRunnerService.SetDefaultTestInterval(timeSpan);
        }

        public void AddTestResultStorage(ITestResultStorage testResultStorage)
        {
            var testRunnerService = _testRunnerServiceAccessor.Value;

            testResultStorage.Save(testRunnerService.TestResults);
        }

        public void RegisterTestResultProcessor(Action<TestResult> processor)
        {
            var testRunnerService = _testRunnerServiceAccessor.Value;

            var observer = new DelegatedObserver<TestResult>(processor);

            testRunnerService.TestResults.Subscribe(observer);
        }
    }
}