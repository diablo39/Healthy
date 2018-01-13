using System;

namespace Healthy.Core.Engine.Tests
{
    public struct TestResult
    {
        public string TestName { get; }

        public TestResultStatus Status { get;  }

        public string Message { get; }

        public TimeSpan RunningTime { get;  }

        public DateTime Moment { get; }

        public TestResult(string testName, TestResultStatus status, TimeSpan runningTime)
        {
            TestName = testName;
            Status = status;
            Message = string.Empty;
            RunningTime = runningTime;
            Moment = DateTime.UtcNow;
        }

        public TestResult(string testName, TestResultStatus status, TimeSpan runningTime, string message)
        {
            TestName = testName;
            Status = status;
            Message = message;
            RunningTime = runningTime;
            Moment = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return 
                $"TestName:     {TestName}{Environment.NewLine}" +
                $"Moment:       {Moment}{Environment.NewLine}" +
                $"Status:       { Status }{Environment.NewLine}" +
                $"RunningTime:  {RunningTime}{Environment.NewLine}" +
                $"Message:      { Message }";
        }
    }
}