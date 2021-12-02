using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cruds.Domain
{
    public class ListaDeClientes
    {

        public List<Cliente> Listagem { get; }

        private static readonly Lazy<ListaDeClientes> lazy =
        new Lazy<ListaDeClientes>(() => new ListaDeClientes());

        public static ListaDeClientes Instance { get { return lazy.Value; } }

        public ListaDeClientes()
        {
            Listagem = new List<Cliente>();

        }

    }
}
