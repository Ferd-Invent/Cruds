using Cruds.Domain;
using Dapper;
using FluentMigrator;
using System.Data.SqlClient;

namespace Cruds.WebViewUI.Migrations
{
    public static class Database_Migration
    {
        public static void EnsureDatabase(string connectionString, string name)
        {
           
            var paramenter = new DynamicParameters();
            paramenter.Add("name", name);
            

            using var connection = new SqlConnection(connectionString);
            var records = connection.Query("SELECT * FROM sys.databases WHERE name=@name", paramenter);

            if (!records.Any())
            {
                connection.Execute($"CREATE DATABASE {name}");

            }
        }
    }
}
