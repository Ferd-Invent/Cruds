using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Cruds.WebViewUI.Models
{
    [Table("Cliente")]
    public class Cliente
    {    
        [Key,Column("Codigo")]
        public int Codigo { get; set; }
        [Column("Nome")]
        public string Nome { get; set; }
        [Column("CPF")]
        public string Cpf { get; set; }
        [Column("DataDeNascimento")]
        public DateTime ? DataDeNascimento { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("RendaMensal")]
        public decimal RendaMensal { get; set; }


    }
}
