using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cruds.Domain
{
    public interface IRepositoryCliente
    {
        public List<Cliente> RetornarClientes();
        public Cliente ExibirPorCodigo(int Id);
        public void AdicionarClientes(Cliente cliente);
        public void AlterarCliente(Cliente cliente);
        public void DeletarCliente(int codigo);

    }
}
