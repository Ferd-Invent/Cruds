using Cruds.Domain;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace Cruds.WebViewUI
{
    public class AppDataConnection : DataConnection
    {
        public ITable<Cliente> Clientes { get { return this.GetTable<Cliente>(); } }
        public AppDataConnection(LinqToDbConnectionOptions<AppDataConnection> options)
        : base(options)
        {

        }
    }
}
