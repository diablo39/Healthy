namespace Healthy.Core.Engine.Tests
{
    public class TestResult
    {
        public TestResultStatus Status { get; private set; }

        public string Message { get; set; }

        public TestResult(TestResultStatus status)
        {
            Status = status;
        }

        public TestResult(TestResultStatus status, string message)
        {
            Status = status;
            Message = message;
        }

        public override string ToString()
        {
            return $"Status: { Status } Message: { Message }";
        }
    }
}