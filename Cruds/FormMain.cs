using Crud;
using Cruds.Domain;
using Cruds.Infra;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;

namespace Crud
{
    public partial class FormMain : Form
    {

        public  RepoLinqTodb linqTodb;
        public IRepositoryCliente _RepositoryCliente;
        
        public FormMain(RepoLinqTodb linqTodb)
        {
            InitializeComponent();
           //this._RepositoryCliente = _RepositoryCliente;
             this.linqTodb = linqTodb;
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente fechar", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Crud crud = new Crud(-1, linqTodb);
            crud.ShowDialog(this);            
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = linqTodb.RetornarClientes();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
           dataGridView1.DataSource = null;
           dataGridView1.DataSource = linqTodb.RetornarClientes();
        }
        public void LoadCostumers()
        {
            

        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hello my Friend, nenhum item foi selecionado!", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
            }
            else
            {
                var Id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);
                
              if (MessageBox.Show("Tem certeza que deseja excluir esse item?", "Alerta!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
                {


                    linqTodb.DeletarCliente(Id);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = linqTodb.RetornarClientes();
                    MessageBox.Show("Item excluido com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Evento Cancelado com sucesso!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Crud crud = new Crud(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()),linqTodb);            
            crud.ShowDialog(this);            
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = linqTodb.RetornarClientes();

        }
    }
}