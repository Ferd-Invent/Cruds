using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cruds.Domain;
using LinqToDB;
using LinqToDB.Data;

namespace Cruds.Infra
{
    public class RepositoryCliente : IRepositoryCliente
    {

        public AcessoBD db = new AcessoBD();
        public static RepositoryCliente _RepositoryCliente;
        public static RepositoryCliente Instance
        {
            get
            {
                if (_RepositoryCliente == null)
                {
                    _RepositoryCliente = new RepositoryCliente();
                }
                return _RepositoryCliente;

            }

        }
        public string GetStringConnection()
        {
            string connectionString = @"Server=localhost\SQLEXPRESS;Database=dbClientes;Trusted_Connection=True;";
            return connectionString;
        }

        public List<Cliente> RetornarClientes()

        {
            string sql = "Select * from Cliente";
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(db.GetStringConnection()))
            {

                using (SqlCommand com = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                    }
                }
            }
            return clientes;

        }

        public Cliente ExibirPorCodigo(int Id)
        {
           
                string sql = "Select * from Cliente where codigo = ";
                Cliente cliente;
                using (SqlConnection con = new SqlConnection(GetStringConnection()))
                {
                    con.Open();

                    sql = sql + Id.ToString();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                        {
                            db.AdicionarParameter("@codigo", Id);
                        cliente = null;
                      }
                    }
                }
                return cliente;

            }
        public void AdicionarClientes(Cliente cliente)
        {
            try
            {
                string sql = "Insert into Cliente(Nome,CPF,DataDeNascimento,Email,RendaMensal) values(@nome,@cpf,@DataDeNascimento,@email,@RendaMensal)";
                db.AdicionarParameter("@nome", cliente.Nome);
                db.AdicionarParameter("@cpf", cliente.Cpf);
                db.AdicionarParameter("@DataDeNascimento", cliente.DataDeNascimento);
                db.AdicionarParameter("@Email", cliente.Email);
                db.AdicionarParameter("@RendaMensal", cliente.RendaMensal);
                db.ExecuteNonQuery(sql);

            }
            catch (Exception ex) { throw ex; }
        }
        public void AlterarCliente(Cliente cliente)
        {

            try
            {
                string sql = "Update Cliente set Nome=@nome, Cpf=@cpf,DataDeNascimento=@DataDeNascimento,Email=@email,RendaMensal=@RendaMensal where codigo=@Codigo";
                db.AdicionarParameter("@nome", cliente.Nome);
                db.AdicionarParameter("@cpf", cliente.Cpf);
                db.AdicionarParameter("@DataDeNascimento", cliente.DataDeNascimento);
                db.AdicionarParameter("@Email", cliente.Email);
                db.AdicionarParameter("@RendaMensal", cliente.RendaMensal);
                db.ExecuteNonQuery(sql);

            }
            catch (Exception ex) { throw ex; }

        }
        public void DeletarCliente(int codigo)
        {
            string sql = "Delete from Cliente where codigo=@codigo";
            db.AdicionarParameter("@codigo", codigo);
            db.ExecuteNonQuery(sql);
        }


      

       
    }
}
