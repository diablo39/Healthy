using Healthy.Core.Engine.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine.Tests
{
    public class TestResultProcessorAggregator : TestResultProcessor
    {
        public override void Process(TestResult result)
        {

        }
    }

    public abstract class TestResultProcessor
    {
        public abstract void Process(TestResult result);
    }
}
