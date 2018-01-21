using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.HealthChecks
{
    partial class HealthCheckController : IDisposable, IObservable<HealthCheckResult>
    {
        private static Random _rnd = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
        private readonly IHealthCheck _healthCheck;
        private Timer _timer;
        private TimeSpan _interval;
        private readonly ILogger<HealthCheckController> _logger;

        internal IList<IObserver<HealthCheckResult>> _observers = new List<IObserver<HealthCheckResult>>(); // TODO: to thread safe collection

        public HealthCheckController(IHealthCheck healthCheck, TimeSpan interval, ILogger<HealthCheckController> logger)
        {
            _healthCheck = healthCheck;
            _interval = interval;
            _logger = logger;
            _timer = new Timer(async o => { await ExecuteHealthCheckAsync((IHealthCheck)o); }, _healthCheck, Timeout.InfiniteTimeSpan, _interval);
        }

        public void Start()
        {
            var delay = _rnd.NextDouble() * _interval.TotalMilliseconds;
            var delayTimeSpan = TimeSpan.FromMilliseconds(delay);
            _timer.Change(delayTimeSpan, _interval);

            _logger.LogInformation("Started health check: {0}, with interval: {1}", _healthCheck.Name, _interval);
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);

            Parallel.For(0, _observers.Count, i => _observers[i].OnCompleted());
        }

        public void SetHealthCheckInterval(TimeSpan interval)
        {
            _interval = interval;
        }

        private async Task ExecuteHealthCheckAsync(IHealthCheck healthCheck)
        {
            try
            {
                var result = await healthCheck.ExecuteAsync();

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
