using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.ConfigurationBuilder
{
    public interface ITestConfigurator
    {
        ITestConfigurator RunEvery(TimeSpan interval);

        ITestConfigurator RunEvery(int seconds);

        ITestConfigurator AddTag(string tag);

        ITestConfigurator AddTags(params string[] tags);
    }
}
