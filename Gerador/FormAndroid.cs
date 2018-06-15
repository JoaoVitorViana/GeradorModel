using Pragma;
using System;
using System.Data;
using System.Windows.Forms;

namespace Utilidades
{
    public partial class FormAndroid : Form
    {
        public FormAndroid()
        {
            InitializeComponent();
            txtPkgObjeto.Text = "models";
            txtVariavelTabela.Text = "mTabela";
        }
        private void btnGerarRecyclerView_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProjeto.Text))
                {
                    MessageBox.Show("Projeto não informado, Verifique!");
                    txtProjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClasse.Text))
                {
                    MessageBox.Show("Classe não informada, Verifique!");
                    txtClasse.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClasseObjeto.Text))
                {
                    MessageBox.Show("Objeto não informado, Verifique!");
                    txtClasseObjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPkgObjeto.Text))
                {
                    MessageBox.Show("Package do objeto não informado, Verifique!");
                    txtPkgObjeto.Focus();
                    return;
                }

                Mobile.GerarRecyclerView(txtProjeto.Text.Trim(), txtClasse.Text.Trim(), txtClasseObjeto.Text.Trim(), txtPkgObjeto.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void cbPkgObjeto_CheckedChanged(object sender, EventArgs e)
        {
            txtPkgObjeto.Enabled = cbPkgObjeto.Checked;
        }
        private void btnSetImagem_Click(object sender, EventArgs e)
        {
            try
            {
                Mobile.GerarSetImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void btnGerarDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProjeto.Text))
                {
                    MessageBox.Show("Projeto não informado, Verifique!");
                    txtProjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClasse.Text))
                {
                    MessageBox.Show("Classe não informada, Verifique!");
                    txtClasse.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtBanco.Text))
                {
                    MessageBox.Show("Banco não informado, Verifique!");
                    txtClasse.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPkgObjeto.Text))
                {
                    MessageBox.Show("Package do objeto não informado, Verifique!");
                    txtPkgObjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtVariavelTabela.Text))
                {
                    MessageBox.Show("Variável Tabela não informado, Verifique!");
                    txtVariavelTabela.Focus();
                    return;
                }

                if (comboTabelas.SelectedIndex == -1)
                {
                    MessageBox.Show("Tabela não selecionada, Verifique!");
                    return;
                }

                Mobile.GerarDB(txtProjeto.Text.Trim(), txtClasse.Text.Trim(), comboTabelas.SelectedValue.ToString(), txtBanco.Text, txtClasseObjeto.Text.Trim(), txtPkgObjeto.Text.Trim(), txtVariavelTabela.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void btnBanco_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Cursor Files|*.db";
                openFileDialog1.Title = "Selecione o bando de dados";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    ComboTabela(Mobile.GetTabelas(openFileDialog1.FileName.ToString()), true, openFileDialog1.FileName.ToString());
                else
                    ComboTabela(null, false, "");
            }
            catch (Exception ex)
            {
                ComboTabela(null, false, "");
                MessageBox.Show(ex.Message);
            }
        }
        private void ComboTabela(DataTable pTabela, bool pHabilita, string pBanco)
        {
            comboTabelas.DataSource = pTabela;
            comboTabelas.Enabled = pHabilita;
            comboTabelas.ValueMember = "Name";
            comboTabelas.DisplayMember = "Name";
            txtBanco.Text = pBanco;
        }
        private void btnGerarFragment_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void btnGerarExpandableList_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProjeto.Text))
                {
                    MessageBox.Show("Projeto não informado, Verifique!");
                    txtProjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClasse.Text))
                {
                    MessageBox.Show("Classe não informada, Verifique!");
                    txtClasse.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClasseObjeto.Text))
                {
                    MessageBox.Show("Objeto principal não informado, Verifique!");
                    txtClasseObjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPkgObjeto.Text))
                {
                    MessageBox.Show("Package do objeto não informado, Verifique!");
                    txtPkgObjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtObjSecundario.Text))
                {
                    MessageBox.Show("Objeto secundario não informado, Verifique!");
                    txtObjSecundario.Focus();
                    return;
                }

                Mobile.GerarExpandableList(txtProjeto.Text.Trim(), txtClasse.Text.Trim(), txtClasseObjeto.Text.Trim(), txtPkgObjeto.Text.Trim(), txtObjSecundario.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void btnGerarArrayAdapter_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProjeto.Text))
                {
                    MessageBox.Show("Projeto não informado, Verifique!");
                    txtProjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClasse.Text))
                {
                    MessageBox.Show("Classe não informada, Verifique!");
                    txtClasse.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClasseObjeto.Text))
                {
                    MessageBox.Show("Objeto principal não informado, Verifique!");
                    txtClasseObjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPkgObjeto.Text))
                {
                    MessageBox.Show("Package do objeto não informado, Verifique!");
                    txtPkgObjeto.Focus();
                    return;
                }

                Mobile.GerarArrayAdapter(txtProjeto.Text.Trim(), txtClasse.Text.Trim(), txtClasseObjeto.Text.Trim(), txtPkgObjeto.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void btnGerarClasseDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProjeto.Text))
                {
                    MessageBox.Show("Projeto não informado, Verifique!");
                    txtProjeto.Focus();
                    return;
                }

                Mobile.GerarClassDB(txtProjeto.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void cbVariavelTabela_CheckedChanged(object sender, EventArgs e)
        {
            txtVariavelTabela.Enabled = cbVariavelTabela.Checked;
        }
        private void btnUtil_Click(object sender, EventArgs e)
        {
            try
            {
                Mobile.GerarConexao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void btnThread_Click(object sender, EventArgs e)
        {
            try
            {
                Mobile.GerarThread();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void btnFireBase_Click(object sender, EventArgs e)
        {
            try
            {
                Mobile.GerarFireBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void comboTabelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtClasse.Text = (comboTabelas.SelectedIndex == -1) ? "" : comboTabelas.SelectedValue.ToString();
            txtClasseObjeto.Text = (comboTabelas.SelectedIndex == -1) ? "" : comboTabelas.SelectedValue.ToString();
        }
        private void btnDialog_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProjeto.Text))
                {
                    MessageBox.Show("Projeto não informado, Verifique!");
                    txtProjeto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClasse.Text))
                {
                    MessageBox.Show("Classe não informada, Verifique!");
                    txtClasse.Focus();
                    return;
                }

                Mobile.GerarDialog(txtProjeto.Text, txtClasse.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}