using System;
using System.Threading.Tasks;
using Healthy.Core;
using Healthy.Core.Engine.HealthChecks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Healthy.HealthChecks.SqlServer
{
    internal class SqlHealthCheck : IHealthCheck
    {
        private const string DefaultSqlQuery = "select 1;";

        private readonly string _connectionString;
        private readonly string _sqlQuery;

        public string Name { get; }

        public SqlHealthCheck(string name, string connectionString, string sqlQuery = null)
        {
            Name = name;
            _connectionString = connectionString;
            _sqlQuery = string.IsNullOrWhiteSpace(sqlQuery) ? DefaultSqlQuery : sqlQuery;
        }

        public async Task<HealthCheckResult> ExecuteAsync()
        {
            var sw = Stopwatch.StartNew();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = _sqlQuery;
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return new HealthCheckResult(Name, HealthCheckResultStatus.Success, sw.Elapsed);
            }
            catch (Exception e)
            {
                return new HealthCheckResult(Name, HealthCheckResultStatus.Failed, sw.Elapsed, e.Message);
            }
            finally
            {
                sw.Stop();
            }
        }
    }
}