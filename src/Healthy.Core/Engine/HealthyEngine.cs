using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace Healthy.Core.Engine
{
    public partial class HealthyEngine
    {
        private TimeSpan _defaultTestInterval = TimeSpan.FromSeconds(15);

        private bool _isRunning = false;

        private List<Timer> _timers = new List<Timer>();

        private List<ITest> _tests = new List<ITest>();

        public IEnumerable<ITest> Tests => _tests.AsReadOnly();

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

        }

        public void SetDefaultTestInterval(TimeSpan interval)
        {

        }

        internal void Run()
        {
            // TODO: set Timer
            for (int i = 0; i < _tests.Count; i++)
            {
                var test = _tests[i];
                RunTest(test);
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
            // store result
        }
    }
}