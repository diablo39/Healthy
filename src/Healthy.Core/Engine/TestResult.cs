namespace Healthy.Core.Engine
{
    public class TestResult
    {
        public TestResultStatus Status { get; private set; }

        public string Message { get; set; }

        public TestResult(TestResultStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}