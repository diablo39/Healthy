using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.Tests
{
    class TestRunner : IDisposable
    {
        private static Random _rnd = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
        private readonly ITest _test;
        private Timer _timer;
        private TimeSpan _interval;
        private readonly Microsoft.Extensions.Logging.ILogger<TestRunner> _logger;

        public TestRunner(ITest test, TimeSpan interval, Microsoft.Extensions.Logging.ILogger<TestRunner> logger)
        {
            _test = test;
            _interval = interval;
            _logger = logger;
            _timer = new Timer(async o => { await ExecuteTest((ITest)o); }, _test, Timeout.InfiniteTimeSpan, _interval);
        }

        public void Start()
        {
            var delay = _rnd.NextDouble() * _interval.TotalMilliseconds;
            var delayTimeSpan = TimeSpan.FromMilliseconds(delay);
            _timer.Change(delayTimeSpan, _interval);
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void SetTestingInterval(TimeSpan interval)
        {
            _interval = interval;
        }

        private async Task ExecuteTest(ITest test)
        {
            var result = await test.ExecuteAsync();

            //_testResultProcessor?.Process(result);
            _logger.LogInformation(result.ToString());
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    _timer.Dispose();
                    _timer = null;
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
