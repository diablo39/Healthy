namespace Healthy.Core.ConfigurationBuilder
{
    public interface ITestsConfigurationBuilder
    {
        void AddTest(string testName, ITest test);
    }
}