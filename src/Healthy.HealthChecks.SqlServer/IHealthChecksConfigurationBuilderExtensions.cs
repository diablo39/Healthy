using System;
using Healthy.Core.ConfigurationBuilder;
using Healthy.HealthChecks.SqlServer;

namespace Healthy.Core
{
    public static class IHealthChecksConfigurationBuilderExtensions
    {
        ///<summary>
        /// Checks if db is available
        ///</summary>
        public static void AddSqlServerHealthCheck(this IHealthChecksConfigurationBuilder cfg, string healthCheckName, string connectionString, Action<IHealthCheckConfigurator> configurator = null)
        {
            cfg.AddHealthCheck(new SqlHealthCheck(healthCheckName, connectionString), configurator);
        }

        ///<summary>
        /// Checks if db is available and runs query against database
        ///</summary>
        public static void AddSqlServerHealthCheck(this IHealthChecksConfigurationBuilder cfg, string healthCheckName, string connectionString, string sqlQuery, Action<IHealthCheckConfigurator> configurator = null)
        {
            cfg.AddHealthCheck(new SqlHealthCheck(healthCheckName, connectionString, sqlQuery), configurator);
        }
    }
}
