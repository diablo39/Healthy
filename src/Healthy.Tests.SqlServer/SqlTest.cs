using System;
using System.Threading.Tasks;
using Healthy.Core;
using Healthy.Core.Engine.Tests;
using System.Data.SqlClient;
using System.Data;

namespace Healthy.Tests.SqlServer
{
    internal class SqlTest : ITest
    {
        private const string DefaultSqlQuery = "select 1;";

        private readonly string _connectionString;
        private readonly string _sqlQuery;

        public SqlTest(string connectionString, string sqlQuery = null)
        {
            _connectionString = connectionString;
            _sqlQuery = string.IsNullOrWhiteSpace(sqlQuery) ? DefaultSqlQuery : sqlQuery;
        }

        public async Task<TestResult> ExecuteAsync()
        {

            System.Console.WriteLine($"Task executing {DateTime.Now}");

            string message = null;
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
            }
            catch (System.Exception e)
            {
                message = e.ToString();

                return new TestResult(TestResultStatus.Failed, message);
            }

            return new TestResult(TestResultStatus.Success);
        }
    }
}