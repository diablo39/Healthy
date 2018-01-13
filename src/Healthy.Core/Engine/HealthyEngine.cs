using System.Collections.Generic;

namespace Healthy.Core.Engine
{
    public class HealthyEngine
    {
        public List<TestBase> _tests = new List<TestBase>();

        public IEnumerable<TestBase> Tests => _tests.AsReadOnly();

        public void AddTest(TestBase test)
        {
            _tests.Add(test);
        }
    }
}