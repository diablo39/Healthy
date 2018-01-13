using System;
using Microsoft.AspNetCore.Builder;
using Healthy.Core.Engine;

namespace Healthy.Core.ConfigurationBuilder
{
    internal partial class HealthyConfigurationBuilder
    {
        public void AddHttpPanel(string path)
        {
            throw new NotImplementedException();
        }

        public void AddHealthCheckUrl(string path)
        {
            throw new NotImplementedException();
        }

        public void AddHeartBeat(string url, int interval, string method, bool sendWhenTestFails = false)
        {
            throw new NotImplementedException();
        }
    }
}