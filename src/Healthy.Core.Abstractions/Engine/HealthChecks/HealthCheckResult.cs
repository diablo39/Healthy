using System;

namespace Healthy.Core.Engine.HealthChecks
{
    public struct HealthCheckResult
    {
        public string HealthCheckName { get; }

        public HealthCheckResultStatus Status { get; }

        public string Message { get; }

        public TimeSpan RunningTime { get; }

        public DateTime Moment { get; }

        public HealthCheckResult(string healthCheckName, HealthCheckResultStatus status, TimeSpan runningTime)
        {
            HealthCheckName = healthCheckName;
            Status = status;
            Message = string.Empty;
            RunningTime = runningTime;
            Moment = DateTime.UtcNow;
        }

        public HealthCheckResult(string healthCheckName, HealthCheckResultStatus status, TimeSpan runningTime, string message)
        {
            HealthCheckName = healthCheckName;
            Status = status;
            Message = message;
            RunningTime = runningTime;
            Moment = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return
                $"healthCheckName: { HealthCheckName }{Environment.NewLine}" +
                $"Moment:          { Moment }{Environment.NewLine}" +
                $"Status:          { Status }{Environment.NewLine}" +
                $"RunningTime:     { RunningTime }{Environment.NewLine}" +
                $"Message:         { Message }";
        }
    }
}