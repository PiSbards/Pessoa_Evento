using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CadastroPessoas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            var fechar = MessageBox.Show("Deseja realmente fechar o programa?", "Fechar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
            if ( fechar == DialogResult.No)
            {
                return;
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            funcoes pessoa = new funcoes();
            List<funcoes> pessoas = pessoa.listaPessoa();
            dgvPessoa.DataSource = pessoas;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            txtID.Focus();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            funcoes pes = new funcoes();
            if (txtNome.Text == "" || txtCelular.Text == "" || txtCidade.Text == "" || txtCPF.Text == "" || txtNascimento.Text == "")
            {
                MessageBox.Show("FALTA DE INFORMAÇÕES, POR FAVOR PREENCHA TODOS OS CAMPOS", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (pes.RegistroRepetido(txtNome.Text, txtCelular.Text, txtCidade.Text, txtCPF.Text, txtNascimento.Text) == true)
            {
                MessageBox.Show("Cadastro já existente em nossa base de dados!");
                txtNome.Text = "";
                txtCelular.Text = "";
            }            
            else
            {
                pes.Inserir(txtNome.Text, txtCelular.Text, txtCidade.Text, txtCPF.Text, txtNascimento.Text);
                MessageBox.Show("Pessoa cadastrada com sucesso!");
                List<funcoes> pessoas = pes.listaPessoa();
                dgvPessoa.DataSource = pessoas;
                txtNome.Text = "";
                txtCelular.Text = "";
                txtCidade.Text = "";
                txtCPF.Text = "";
                txtNascimento.Text = "";
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text.Trim());
            if (txtID.Text == "")
            {
                MessageBox.Show("FALTA DE INFORMAÇÕES, POR FAVOR PREENCHA TODOS OS CAMPOS", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtNome.Text != null)
            {
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
            }
            funcoes pes = new funcoes();
            pes.Localizar(id);
            MessageBox.Show($"Nome:{pes.nome}" +
                $"Celular:{pes.celular}" +
                $"Cidade:{pes.cidade}" +
                $"CPF:{pes.cpf}" +
                $"Data de Nascimento:{pes.dataNascimento}", "Resultado da Busca");
            txtNome.Text = pes.nome;
            txtCelular.Text = pes.celular;
            txtCidade.Text = pes.cidade;
            txtCPF.Text = pes.cpf;
            txtNascimento.Text = pes.dataNascimento;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text.Trim());
            var editar = MessageBox.Show("Deseja realmente editar o registro?", "Editar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (editar == DialogResult.No)
            {
                return;
            }
            else
            {
                funcoes pes = new funcoes();
                pes.Atualizar(id, txtNome.Text, txtCelular.Text, txtCidade.Text, txtCPF.Text, txtNascimento.Text);
                MessageBox.Show("Cadastro atualizado com sucesso!");
                List<funcoes> pessoas = pes.listaPessoa();
                dgvPessoa.DataSource = pessoas;
                txtNome.Text = "";
                txtCelular.Text = "";
                txtCidade.Text = "";
                txtCPF.Text = "";
                txtNascimento.Text = "";
            }
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text.Trim());
            var excluir = MessageBox.Show("Deseja realmente excluir o registro?", "Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (excluir == DialogResult.No)
            {
                return;
            }
            else
            {
                funcoes pes = new funcoes();
                pes.Excluir(id);
                MessageBox.Show("Cadastro excluído com sucesso!");
                List<funcoes> pessoas = pes.listaPessoa();
                dgvPessoa.DataSource = pessoas;
                txtNome.Text = "";
                txtCelular.Text = "";
                txtCidade.Text = "";
                txtCPF.Text = "";
                txtNascimento.Text = "";
            }
        }

        private void dgvPessoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvPessoa.Rows[e.RowIndex];
                this.dgvPessoa.Rows[e.RowIndex].Selected = true;
                txtID.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtCelular.Text = row.Cells[2].Value.ToString();
                txtCidade.Text = row.Cells[3].Value.ToString();
                txtCPF.Text = row.Cells[4].Value.ToString();
                txtNascimento.Text = row.Cells[5].Value.ToString();
            }
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }
    }
}
