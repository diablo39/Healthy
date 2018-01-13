using System;
using Healthy.Core.ConfigurationBuilder;
using Healthy.Tests.Redis;

namespace Healthy.Core
{
    public static class TestsConfigurationBuilderExtensions
    {
        public static void AddRedisTest(this ITestsConfigurationBuilder cfg, string s1, string s2)
        {

        }

        public static void AddRedisTest(this ITestsConfigurationBuilder cfg, string s1, string s2, Action<RedisTestConfiguration> redisTestConfiguration)
        {

        }
    }
}
