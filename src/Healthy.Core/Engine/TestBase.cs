using System.Threading.Tasks;

namespace Healthy.Core.Engine
{
    public abstract class TestBase
    {
        public abstract Task<TestResult> ExecuteAsync();
    }
}