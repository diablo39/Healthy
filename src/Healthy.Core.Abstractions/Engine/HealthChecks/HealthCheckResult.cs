using System;
using System.Collections.Generic;

namespace Healthy.Core.Engine.HealthChecks
{
    public struct HealthCheckResult
    {
        public static HealthCheckResult Empty = new HealthCheckResult();

        public string HealthCheckId { get; }

        public string HealthCheckName { get; }

        public HealthCheckResultStatus Status { get; }

        public string Message { get; }

        public TimeSpan RunningTime { get; }

        public DateTime Moment { get; }

        //public Exception Exception { get; }

        public HealthCheckResult(string healthCheckId, string healthCheckName, HealthCheckResultStatus status, TimeSpan runningTime, string message = null)
        {
            HealthCheckId = healthCheckId;
            HealthCheckName = healthCheckName;
            Status = status;
            Message = message;
            RunningTime = runningTime;
            Moment = DateTime.UtcNow;
            //Exception = null;
        }

        public override string ToString()
        {
            return string.Concat(
                $"HealthCheckName: { HealthCheckName }{Environment.NewLine}",
                $"Moment:          { Moment }{Environment.NewLine}",
                $"Status:          { Status }{Environment.NewLine}",
                $"RunningTime:     { RunningTime }{Environment.NewLine}",
                $"Message:         { Message }");
        }

        public override bool Equals(object obj)
        {
            if (!(obj is HealthCheckResult))
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            var hashCode = -1276013893;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HealthCheckId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HealthCheckName);
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            hashCode = hashCode * -1521134295 + EqualityComparer<TimeSpan>.Default.GetHashCode(RunningTime);
            hashCode = hashCode * -1521134295 + Moment.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(HealthCheckResult c1, HealthCheckResult c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(HealthCheckResult c1, HealthCheckResult c2)
        {
            return !c1.Equals(c2);
        }
    }
}