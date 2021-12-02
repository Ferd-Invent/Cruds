using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cruds.Domain
{
    public interface ICliente
    {

        int Codigo { get; set; }

        string Nome { get; set; }

        string Cpf { get; set; }

        DateTime DataDeNascimento { get; set; }

        string Email { get; set; }

        decimal RendaMensal { get; set; }

      
        



    } 
}
