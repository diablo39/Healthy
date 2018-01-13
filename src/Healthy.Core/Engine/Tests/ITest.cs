using Healthy.Core.Engine.Tests;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.Tests
{
    public interface ITest
    {
        Task<TestResult> ExecuteAsync();
    }
}