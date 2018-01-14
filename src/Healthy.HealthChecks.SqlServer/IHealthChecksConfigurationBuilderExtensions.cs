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
        public static IHealthCheckConfigurator AddSqlServerHealthCheck(this IHealthChecksConfigurationBuilder cfg, string healthCheckName, string connectionString)
        {
            return cfg.AddHealthCheck(new SqlHealthCheck(healthCheckName, connectionString));
        }

        ///<summary>
        /// Checks if db is available and runs query against database
        ///</summary>
        public static IHealthCheckConfigurator AddSqlServerHealthCheck(this IHealthChecksConfigurationBuilder cfg, string healthCheckName, string connectionString, string sqlQuery)
        {
            return cfg.AddHealthCheck(new SqlHealthCheck(healthCheckName, connectionString, sqlQuery));
        }
    }
}
