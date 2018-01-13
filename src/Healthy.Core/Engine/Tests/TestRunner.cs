using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.Tests
{
    partial class TestRunner : IDisposable, IObservable<TestResult>
    {
        private static Random _rnd = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
        private readonly ITest _test;
        private Timer _timer;
        private TimeSpan _interval;
        private readonly ILogger<TestRunner> _logger;

        internal IList<IObserver<TestResult>> _observers = new List<IObserver<TestResult>>(); // TODO: to thread safe collection

        public TestRunner(ITest test, TimeSpan interval, ILogger<TestRunner> logger)
        {
            _test = test;
            _interval = interval;
            _logger = logger;
            _timer = new Timer(async o => { await ExecuteTestAsync((ITest)o); }, _test, Timeout.InfiniteTimeSpan, _interval);
        }

        public void Start()
        {
            var delay = _rnd.NextDouble() * _interval.TotalMilliseconds;
            var delayTimeSpan = TimeSpan.FromMilliseconds(delay);
            _timer.Change(delayTimeSpan, _interval);

            _logger.LogInformation("Started test: {0}, with interval: {1}", _test.TestName, _interval);
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);

            Parallel.For(0, _observers.Count, i => _observers[i].OnCompleted());
        }

        public void SetTestingInterval(TimeSpan interval)
        {
            _interval = interval;
        }

        public IDisposable Subscribe(IObserver<TestResult> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Unsubscriber(this, observer);
        }

        private async Task ExecuteTestAsync(ITest test)
        {
            try
            {
                var result = await test.ExecuteAsync();

                _logger.LogInformation(result.ToString());

                Parallel.For(0, _observers.Count, i => _observers[i].OnNext(result)); // TODO: consider separate try/catch
            }
            catch (Exception ex)
            {
                Parallel.For(0, _observers.Count, i => _observers[i].OnError(ex));
            }
        }
    }
}
