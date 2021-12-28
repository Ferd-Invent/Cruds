using Cruds.Domain;
using Cruds.Infra;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud
{
    public partial class Crud : Form
    {
        public int Indice;
        public List<Cliente> clientes;

        public Cliente cliente1 = new();

        public RepositoryClienteLinq linqTodb;

        public Crud(int fes, RepositoryClienteLinq linqTodb)
        {
            this.linqTodb = linqTodb;

            InitializeComponent();            
            Indice = fes;

            Cliente cliente1;
            // ServicosClientes = ServicosClientes.Instance;
           /// _Kernel = StandardKernel;
            

            if (fes >= 0)
            {  
                cliente1 = linqTodb.ExibirPorCodigo(fes);
                txtCodigo.Text = cliente1.Codigo.ToString();
                txtNome.Text = cliente1.Nome.ToString();
                mtbCPF.Text = cliente1.Cpf.ToString();  
                dtpDataNascimento.Value = Convert.ToDateTime(cliente1.DataDeNascimento.ToString());
                txtEmail.Text = cliente1.Email.ToString();
                nudRendaMensal.Value = Convert.ToDecimal(cliente1.RendaMensal.ToString());
            }
            else
            {
                txtCodigo.Text ="0";
            }
        }

        public void PreencherDadosCliente(Cliente cliente1)
        {
            cliente1.Codigo = Convert.ToInt32(txtCodigo.Text);            
            cliente1.Nome = txtNome.Text;
            cliente1.Cpf = mtbCPF.Text;
            cliente1.DataDeNascimento = dtpDataNascimento.Value.Date;
            cliente1.Email = txtEmail.Text;
            cliente1.RendaMensal = nudRendaMensal.Value;
        }
        public bool TodosOsValoresNulos()
        {
            if (linqTodb.RetornarClientes().Count < 0)
            {
                if (!String.IsNullOrWhiteSpace(txtNome.Text)) return true;
                if (!String.IsNullOrWhiteSpace(mtbCPF.Text)) return true;
                if (dtpDataNascimento.Value.Date != DateTime.Now.Date) return true;
                if (!String.IsNullOrWhiteSpace(txtEmail.Text)) return true;
                if (nudRendaMensal.Value > 0) return true;

                return false;

            }
            else
            {
                if (String.IsNullOrWhiteSpace(txtNome.Text)) return true;
                if (String.IsNullOrWhiteSpace(mtbCPF.Text)) return true;
                if (dtpDataNascimento.Value.Date == DateTime.Now.Date) return true;
                if (String.IsNullOrWhiteSpace(txtEmail.Text)) return true;
                if (nudRendaMensal.Value == 0) return true;

                return false;

            }
        }
        //public void PreencherCampos(Cliente cliente)
        //{
        //    if (Indice < 0)
        //    {
        //        cliente.Codigo = clientes.Count + 1;
        //        cliente.Nome = txtNome.Text;
        //        cliente.Cpf = mtbCPF.Text;
        //        cliente.DataDeNascimento = dtpDataNascimento.Value.Date;
        //        cliente.Email = txtEmail.Text;
        //        cliente.RendaMensal = nudRendaMensal.Value;

        //      //  deClientes.Listagem.Add(cliente);
        //    }
        //    else
        //    {
        //        //[Indice].Nome = txtNome.Text;
        //        //deClientes.Listagem[Indice].Cpf = mtbCPF.Text;
        //        //deClientes.Listagem[Indice].DataDeNascimento = dtpDataNascimento.Value.Date;
        //        //deClientes.Listagem[Indice].Email = txtEmail.Text;
        //        //deClientes.Listagem[Indice].RendaMensal = nudRendaMensal.Value;
                

        //    }
        //}
        public void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            mtbCPF.Clear();
            dtpDataNascimento.Value = DateTime.Now.Date;
            nudRendaMensal.Value = 0;

        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (txtCodigo.Text !="0" )
            {
               PreencherDadosCliente(cliente1);
               linqTodb.AlterarCliente(cliente1);
               MessageBox.Show("Seu cadastro foi alterado com sucesso", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {                 
               PreencherDadosCliente(cliente1);
               linqTodb.AdicionarClientes(cliente1);
               MessageBox.Show("Seu cadastro foi realizado com sucesso", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Close();
            LimparCampos();
           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}

