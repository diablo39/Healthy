using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthy.Core.Engine
{
    public class HealthyEngine
    {
        public List<ITest> _tests = new List<ITest>();

        public IEnumerable<ITest> Tests => _tests.AsReadOnly();

        public void AddTest(string testName, ITest test)
        {
            _tests.Add(test);
        }

        internal async Task RunAsync()
        {
            for (int i = 0; i < _tests.Count; i++)
            {
                var test = _tests[i];

                var result = await test.ExecuteAsync();
            }
        }
    }
}