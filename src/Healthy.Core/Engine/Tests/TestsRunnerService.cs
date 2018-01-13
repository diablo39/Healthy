using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.Tests
{
    class TestsRunnerService : IService
    {
        private TimeSpan _defaultTestInterval = TimeSpan.FromSeconds(15);

        private List<TestRunner> _testRunners = new List<TestRunner>();

        private bool _isRunning = false;

        private readonly ILoggerFactory _loggerFactory;
        private readonly TestResultProcessor _testResultProcessor;

        public TestsRunnerService(ILoggerFactory loggerFactory, TestResultProcessor testResultProcessor)
        {
            _loggerFactory = loggerFactory;
            _testResultProcessor = testResultProcessor;
        }

        public void AddTest(ITest test)
        {
            lock (_testRunners)
            {
                var testRunner = new TestRunner(test, _defaultTestInterval, _loggerFactory.CreateLogger<TestRunner>());
                _testRunners.Add(testRunner);

                if (_isRunning)
                {
                    testRunner.Start();
                }
            }
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

            lock (_testRunners)
            {
                _isRunning = true;

                for (int i = 0; i < _testRunners.Count; i++)
                {
                    var testRunner = _testRunners[i];
                    testRunner.Start();
                }
            }
        }

        public void Stop()
        {
            if (!_isRunning) return;

            lock (_testRunners)
            {
                for (int i = 0; i < _testRunners.Count; i++)
                {
                    var testRunner = _testRunners[i];
                    testRunner.Stop();
                }

                _isRunning = false;
            }
        }

        public void Dispose()
        {
            lock (_testRunners)
            {
                while (_testRunners.Count > 0)
                {
                    var testRunner = _testRunners[0];
                    _testRunners.RemoveAt(0);
                    testRunner.Dispose();
                }
                _isRunning = false;
            }
        }
    }
}
