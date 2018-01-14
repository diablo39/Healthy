namespace Healthy.Core.ConfigurationBuilder
{
    public interface IOutputConfigurationBuilder
    {
        void AddHttpPanel(string path);

        void AddHealthCheckUrl(string path);
    }
}