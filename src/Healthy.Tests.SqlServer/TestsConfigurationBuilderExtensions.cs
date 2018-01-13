using System;
using Healthy.Core.ConfigurationBuilder;
using Healthy.Tests.SqlServer;

namespace Healthy.Core
{
    public static class TestsConfigurationBuilderExtensions
    {
        ///<summary>
        /// Tests if db is available
        ///</summary>
        public static void AddSqlServerTest(this ITestsConfigurationBuilder cfg, string testName, string connectionString)
        {
            cfg.AddTest(new SqlTest(testName, connectionString));
        }

        ///<summary>
        /// Tests if db is available and runs query against database
        ///</summary>
        public static void AddSqlServerTest(this ITestsConfigurationBuilder cfg, string testName, string connectionString, string sqlQuery)
        {
            cfg.AddTest(new SqlTest(testName, connectionString, sqlQuery));
        }
    }
}
