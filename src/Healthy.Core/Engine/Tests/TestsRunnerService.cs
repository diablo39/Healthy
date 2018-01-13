using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.Tests
{
    class TestsRunnerService : IService
    {
        private TimeSpan _defaultTestInterval = TimeSpan.FromSeconds(15);

        private ConcurrentBag<TestRunner> _testRunners = new ConcurrentBag<TestRunner>();

        private bool _isRunning = false;

        private readonly ILoggerFactory _loggerFactory;

        private readonly TestResultProcessor _testResultProcessor;

        private TestResultObserver _observer;

        internal IObservable<TestResult> TestResults { get => _observer; }

        public TestsRunnerService(ILoggerFactory loggerFactory, TestResultProcessor testResultProcessor)
        {
            _loggerFactory = loggerFactory;
            _testResultProcessor = testResultProcessor;
            _observer = new TestResultObserver();
        }

        public TestRunner AddTest(ITest test)
        {
            var testRunner = new TestRunner(test, _defaultTestInterval, _loggerFactory.CreateLogger<TestRunner>());
            testRunner.Subscribe(_observer);
            _testRunners.Add(testRunner);

            if (_isRunning)
            {
                testRunner.Start();
            }

            return testRunner;
        }

        public void SetDefaultTestInterval(int interval)
        {
            SetDefaultTestInterval(TimeSpan.FromSeconds(interval));
        }

        public void SetDefaultTestInterval(TimeSpan interval)
        {
            _defaultTestInterval = interval;
        }

        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;

            foreach (var testRunner in _testRunners)
            {
                testRunner.Start();
            }
        }

        public void Stop()
        {
            if (!_isRunning) return;

            _isRunning = false;

            foreach (var testRunner in _testRunners)
            {
                testRunner.Stop();
            }
        }

        public void Dispose()
        {
            _isRunning = false;
            TestRunner testRunner = null;
            while (_testRunners.TryTake(out testRunner))
            {
                testRunner.Dispose();
            }
        }
    }
}
