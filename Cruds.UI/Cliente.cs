using LinqToDB.Mapping;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cruds.Domain
{
    [Table(Schema = "dbo", Name = "Cliente")]
    public class Cliente : ICliente
    {
        [PrimaryKey, Identity]
        public int Codigo { get; set; }

        [Column, Nullable]
        public string Nome { get; set; }

        [Column, Nullable]
        public string Cpf { get; set; }

        [Column, Nullable]
        public DateTime DataDeNascimento { get; set; }

        [Column, Nullable]
        public string Email { get; set; }

        [Column, Nullable]
        [System.ComponentModel.DataAnnotations.DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        public decimal RendaMensal { get; set; }

        public Cliente(int codigo, string nome, string cpf, DateTime dataDeNascimento, string email, decimal rendaMensal)
        {
            Codigo = codigo;
            Nome = nome;
            Cpf = cpf;
            DataDeNascimento = dataDeNascimento;
            Email = email;
            RendaMensal = rendaMensal;
        }
        public Cliente()
        {
           
        }



    }

        

        
        
        
}


    


