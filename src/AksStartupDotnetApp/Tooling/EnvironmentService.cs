using System;

namespace Tooling
{
    public static class EnvironmentService
    {

        public static string GetSqlConnectionString()
        {
            return Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
        }
    }
}
