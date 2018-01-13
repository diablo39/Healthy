using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.Tests
{
    class TestsRunner : IService
    {
        private TimeSpan _defaultTestInterval = TimeSpan.FromSeconds(15);

        private List<Timer> _timers = new List<Timer>();

        private List<ITest> _tests = new List<ITest>();

        private bool _isRunning = false;

        private readonly ILogger<TestsRunner> _logger;

        private readonly TestResultProcessor _testResultProcessor;

        public IEnumerable<ITest> Tests => _tests.AsReadOnly();

        public TestsRunner(ILogger<TestsRunner> logger, TestResultProcessor testResultProcessor)
        {
            _logger = logger;
            _testResultProcessor = testResultProcessor;
        }

        public void AddTest(string testName, ITest test)
        {
            _tests.Add(test);

            if (_isRunning)
            {
                RunTest(test);
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

            _isRunning = true;

            for (int i = 0; i < _tests.Count; i++)
            {
                var test = _tests[i];
                RunTest(test);
            }
        }

        public void Stop()
        {
            if (!_isRunning) return;

            lock (_timers)
            {
                while (_timers.Count > 0)
                {
                    var timer = _timers[0];
                    _timers.RemoveAt(0);
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    timer.Dispose();
                }
                _isRunning = false;
            }
        }

        private void RunTest(ITest test)
        {
            lock (_timers)
            {
                var timer = new Timer(async o => { await ExecuteTest((ITest)o); }, test, TimeSpan.FromSeconds(0), _defaultTestInterval);
                _timers.Add(timer);
            }
        }

        private async Task ExecuteTest(ITest test)
        {
            var result = await test.ExecuteAsync();

            _testResultProcessor?.Process(result);

            _logger.LogInformation(result.ToString());
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
