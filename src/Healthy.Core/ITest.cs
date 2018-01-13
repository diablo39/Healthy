using Healthy.Core.Engine.Tests;
using System.Threading.Tasks;

namespace Healthy.Core
{
    public interface ITest
    {
        Task<TestResult> ExecuteAsync();
    }
}