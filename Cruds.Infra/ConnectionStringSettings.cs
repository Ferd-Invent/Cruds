using LinqToDB.Configuration;

namespace Cruds.Infra
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }
    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders
            => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "dbClientes",
                        ProviderName = "System.Data.SqlClient",
                        ConnectionString =
                            @"Server=localhost\SQLEXPRESS;Database=dbClientes;Trusted_Connection=True;"
                    };
            }
        }
    }
}