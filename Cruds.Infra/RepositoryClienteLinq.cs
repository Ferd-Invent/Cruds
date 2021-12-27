using Cruds.Domain;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using System.Data.SqlClient;
using DataModels;

namespace Cruds.Infra
{
    public class RepositoryClienteLinq : IRepositoryCliente
    {

        public List<Cliente> RetornarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (var db = new DbClientesDB())
            {
                var q =
                    from c in db.Clientes
                    select c;

                foreach (var c in q)
                {
                    clientes.Add(c);
                }
                return clientes;
            }
       }
        public Cliente ExibirPorCodigo(int Id)
        {

            Cliente cliente = new Cliente();

            using (var db = new DbClientesDB())
            {
                return db
                    .Clientes
                    .FirstOrDefault(x => x.Codigo == Id);

            }
        }
        public void AdicionarClientes(Cliente cliente)
        {
            using (var db = new DbClientesDB())

            {

                db.GetTable<Cliente>().Insert(() => new Cliente
                {
                    Nome = cliente.Nome,
                    Cpf = cliente.Cpf,
                    DataDeNascimento = cliente.DataDeNascimento,
                    Email = cliente.Email,
                    RendaMensal = cliente.RendaMensal
                });
            }
        }
        public void AlterarCliente(Cliente cliente)
        {
            using (var db = new DbClientesDB())
            {
                 db.GetTable<Cliente>()
                  .Where(t => t.Codigo == cliente.Codigo)
                  .Update(t => new Cliente
                  {
                      Nome = cliente.Nome,
                      Cpf = cliente.Cpf,
                      DataDeNascimento = cliente.DataDeNascimento,
                      Email = cliente.Email,
                      RendaMensal = cliente.RendaMensal
                  });
            }
        }
        public void DeletarCliente(int codigo)
        {
            using (var db = new DbClientesDB())
            {
                db.GetTable<Cliente>().Where(t => t.Codigo == codigo).Delete();

            }
        }
       
        
    }
}
