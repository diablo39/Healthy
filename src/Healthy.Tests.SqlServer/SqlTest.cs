using System;
using System.Threading.Tasks;
using Healthy.Core;
using Healthy.Core.Engine.Tests;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Healthy.Tests.SqlServer
{
    internal class SqlTest : ITest
    {
        private const string DefaultSqlQuery = "select 1;";

        private readonly string _connectionString;
        private readonly string _sqlQuery;

        public string TestName { get; }

        public SqlTest(string testName, string connectionString, string sqlQuery = null)
        {
            TestName = testName;
            _connectionString = connectionString;
            _sqlQuery = string.IsNullOrWhiteSpace(sqlQuery) ? DefaultSqlQuery : sqlQuery;
        }

        public async Task<TestResult> ExecuteAsync()
        {
            var sw = Stopwatch.StartNew();
            string message = null;
            TestResult result;
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

                result =  new TestResult(TestName, TestResultStatus.Failed, sw.Elapsed, message);
            }

            result = new TestResult(TestName, TestResultStatus.Success, sw.Elapsed);

            sw.Stop();

            return result;
        }
    }
}