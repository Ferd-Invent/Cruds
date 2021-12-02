using Cruds.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cruds.Infra
{
    public class ServicosClientes
    {
        public AcessoBD db;
        public static ServicosClientes servicos;



        public static ServicosClientes Instance
        {
            get
            {
                if (servicos == null)
                {
                    return servicos = new ServicosClientes();
                }

                return servicos;
            }
        }
        public ServicosClientes()
        {
            db = AcessoBD.Instance;
            
        }
        public void AdicionarClientes(string nome, string cpf, DateTime DataDeNascimento, string email, decimal RendaMensal)
        {
            try
            {
                string sql = "Insert into Cliente(Nome,CPF,DataDeNascimento,Email,RendaMensal) values(@nome,@cpf,@DataDeNascimento,@email,@RendaMensal)";
                db.AdicionarParameter("@nome", nome);
                db.AdicionarParameter("@cpf", cpf);
                db.AdicionarParameter("@DataDeNascimento", DataDeNascimento);
                db.AdicionarParameter("@Email", email);
                db.AdicionarParameter("@RendaMensal", RendaMensal);
                db.ExecuteNonQuery(sql);

            }
            catch (Exception ex) { throw ex; }
        }
        public void AlterarCliente(string nome, string cpf, DateTime DataDeNascimento, string email, decimal RendaMensal, int codigo)
        {

            try
            {
                string sql = "Update Cliente set Nome=@nome, Cpf=@cpf,DataDeNascimento=@DataDeNascimento,Email=@email,RendaMensal=@RendaMensal where codigo=@Codigo";
                db.AdicionarParameter("@nome", nome);
                db.AdicionarParameter("@cpf", cpf);
                db.AdicionarParameter("@DataDeNascimento", DataDeNascimento);
                db.AdicionarParameter("@Email", email);
                db.AdicionarParameter("@RendaMensal", RendaMensal);
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


        public DataTable ExibirPorCodigo(int Id)
        {
            return db.ExibirPorCodigo(Id);

        }

        public DataTable GetAllClientes()

        {
            string sql = "Select * from Cliente";
            var datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.GetStringConnection()))
            {

                using (SqlCommand com = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        adapter.Fill(datatable);
                        
                    }
                }
            }
            return datatable;

        }
    }
}
