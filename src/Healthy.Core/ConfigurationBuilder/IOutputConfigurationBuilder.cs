namespace Healthy.Core.ConfigurationBuilder
{
    public interface IOutputConfigurationBuilder
    {
        void AddHttpPanel(string path);

        void AddHealthCheckUrl(string path);

        void AddHeartBeat(string url, int interval, string method, bool sendWhenTestFails = false);
    }
}