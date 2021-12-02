using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Cruds.Infra
{
    public class AcessoBD
    {
        public DbConnection dbConnection;
        public DbCommand dbCommand;
        public DbProviderFactory objFator = null;

        public static AcessoBD acessoBD;
        public enum EstadoConexao
        {
            ConexaoAberta,
            FechadaConexaoAoSair
        }
        public string GetStringConnection()
        {
            string connectionString = @"Server=localhost\SQLEXPRESS;Database=dbClientes;Trusted_Connection=True;";
            return connectionString;
        }
        public AcessoBD()
        {
            objFator = SqlClientFactory.Instance;
            dbConnection = objFator.CreateConnection();
            dbCommand = objFator.CreateCommand();
            dbConnection.ConnectionString = GetStringConnection();
            dbCommand.Connection = dbConnection;

        }
        public static AcessoBD Instance
        {
            get
            {
                if (acessoBD == null)
                {
                    acessoBD = new AcessoBD();
                    return acessoBD;

                }
                return acessoBD;

            }
        }
        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text, EstadoConexao.FechadaConexaoAoSair);

        }
        public int ExecuteNonQuery(string query, CommandType commandType)
        {
            return ExecuteNonQuery(query, commandType, EstadoConexao.FechadaConexaoAoSair);
        }

        public int ExecuteNonQuery(string query, EstadoConexao estadoConexao)
        {
            return ExecuteNonQuery(query, CommandType.Text, estadoConexao);
        }
        public int ExecuteNonQuery(string query, CommandType commandType, EstadoConexao estadoConexao)
        {
            dbCommand.CommandText = query;
            dbCommand.CommandType = commandType;
            int i = -1;

            try
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                    i = dbCommand.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand.Parameters.Clear();
                if (estadoConexao == EstadoConexao.FechadaConexaoAoSair)
                {
                    dbConnection.Close();
                }

            }
            return i;
        }

        public int AdicionarParameter(string name, object value)
        {
            DbParameter p = objFator.CreateParameter();
            p.ParameterName = name;
            p.Value = value;

            return dbCommand.Parameters.Add(p);
        }
        public int AdicionarParameter(DbParameter parameter)
        {
            return dbCommand.Parameters.Add(parameter);
        }
        //public DbDataReader ExecuteReader(string query)
        //{
        //    return ExecuteReader(query, CommandType.Text, ConnectionState.Closed);
        //}
        // public DbDataReader ExecuteReader(string query, CommandType commandtype)
        // {
        //     return ExecuteReader(query, commandtype, ConnectionState.FechaConexaoAoSair);
        // }
        // public DbDataReader ExecuteReader(string query, ConnectionState connectionstate)
        // {
        //     return ExecuteReader(query, CommandType.Text, connectionstate);
        // }

        //public DbDataReader ExecuteReader(string query, CommandType commandtype,
        //     ConnectionState connectionstate)
        // {


        //     objCommand.CommandText = query;
        //     objCommand.CommandType = commandtype;
        //     DbDataReader reader = null;
        //     try
        //     {
        //         if (objConnection.State == System.Data.ConnectionState.Closed)
        //             objConnection.Open();
        //         if (connectionstate == ConnectionState.Closed)
        //             reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //         else
        //             reader = objCommand.ExecuteReader();
        //     }
        //     catch (Exception ex)
        //     {
        //         throw (ex);
        //     }
        //     finally
        //     {
        //         objCommand.Parameters.Clear();
        //     }
        //     return reader;
        // }

        public DataTable ExibirPorCodigo(int Id)
        {
            string sql = "Select * from Cliente where codigo = ";
            var datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(GetStringConnection()))
            {
                con.Open();

                sql = sql + Id.ToString();
                using (SqlCommand com = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        AdicionarParameter("@codigo", Id);
                        adapter.Fill(datatable);
                    }
                }
            }
            return datatable;

        }



    }


}