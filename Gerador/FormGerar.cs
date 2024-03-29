﻿using Pragma.DataBase;
using Pragma.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Gerador
{
    public partial class FormGerar : Form
    {
        public FormGerar()
        {
            InitializeComponent();
        }

        private void comboDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedIndex != -1)
            {
                switch (comboBox.SelectedItem.ToString())
                {
                    case "Sql Server":
                        comboTipoLogon.SelectedIndex = 0;
                        comboTipoLogon.Enabled = true;
                        break;
                    case "My Sql":
                        comboTipoLogon.SelectedIndex = 1;
                        comboTipoLogon.Enabled = false;
                        break;
                }
            }
            else
            {
                comboTipoLogon.SelectedIndex = -1;
                comboTipoLogon.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormBanco().Show();
        }

        private void comboTipoLogon_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            bool habilita = false;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    habilita = false;
                    break;
                case 1:
                    habilita = true;
                    break;
            }

            txtUsuario.Enabled = habilita;
            txtSenha.Enabled = habilita;
            txtUsuario.Text = "";
            txtSenha.Text = "";
        }

        private bool Validacao(out DB.TpBanco tipoDB, bool pValidarGeracao = false)
        {
            tipoDB = DB.TpBanco.Nenhum;
            if (comboDB.SelectedIndex == -1)
            {
                MessageBox.Show("Banco de Dados não selecionado, Verifique!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtServidor.Text))
            {
                txtServidor.Focus();
                MessageBox.Show("Servidor não informado, Verifique!");
                return false;
            }

            if (comboTipoLogon.SelectedIndex == -1)
            {
                MessageBox.Show("Tipo Logon não informado, Verifique!");
                return false;
            }

            if (comboTipoLogon.SelectedIndex == 1 && string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Usuario não informado, Verifique!");
                return false;
            }

            if (pValidarGeracao)
            {
                if (comboBanco.SelectedIndex == -1)
                {
                    MessageBox.Show("DataBase não selecionado, Verifique!");
                    return false;
                }

                if (comboLinguagem.SelectedIndex == -1)
                {
                    MessageBox.Show("Linguagem não selecionada, Verifique!");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtPacote.Text))
                {
                    MessageBox.Show(lbPacote.Text.Replace(":", "") + " não informado, Verifique!");
                    txtPacote.Focus();
                    return false;
                }
            }

            switch (comboDB.SelectedItem.ToString())
            {
                case "Sql Server":
                    tipoDB = DB.TpBanco.SqlServer;
                    break;
                case "My Sql":
                    tipoDB = DB.TpBanco.MySql;
                    break;
            }

            return true;
        }

        private void btnDataBases_Click(object sender, EventArgs e)
        {
            try
            {
                DB.TpBanco tipoDB;
                if (Validacao(out tipoDB))
                {
                    if (tipoDB == DB.TpBanco.SqlServer)
                        ComboBanco(SqlServer.GetBancos(txtServidor.Text.Trim(), RetornaUsuario()), true);
                    else if (tipoDB == DB.TpBanco.MySql)
                        ComboBanco(MySql.GetBancos(txtServidor.Text.Trim(), RetornaUsuario()), true);
                    else
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private UserDB RetornaUsuario()
        {
            UserDB usuario = null;
            if (comboTipoLogon.SelectedIndex == 1)
            {
                usuario = new UserDB();
                usuario.Usuario = txtUsuario.Text.Trim();
                usuario.Senha = txtSenha.Text.Trim();
            }
            return usuario;
        }

        private void ComboBanco(DataTable pBancos, bool pHabilita)
        {
            comboBanco.ValueMember = "Name";
            comboBanco.DisplayMember = "Name";
            comboBanco.Enabled = pHabilita;
            comboBanco.DataSource = pBancos;
        }

        private void ComboTabela(DataTable pTabelas, bool pHabilita)
        {
            comboTabela.ValueMember = "Name";
            comboTabela.DisplayMember = "Name";
            comboTabela.Enabled = pHabilita;
            comboTabela.DataSource = pTabelas;
        }

        private void comboBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtServidor.Text.Trim()))
                    ComboTabela(null, false);
                else
                {
                    switch (comboDB.SelectedItem.ToString())
                    {
                        case "Sql Server":
                            ComboTabela(SqlServer.GetTabelas(txtServidor.Text.Trim(), comboBanco.SelectedValue.ToString(), RetornaUsuario()), true);
                            break;
                        case "My Sql":
                            ComboTabela(MySql.GetTabelas(txtServidor.Text.Trim(), comboBanco.SelectedValue.ToString(), RetornaUsuario()), true);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ComboTabela(null, false);
            }
        }

        private bool ValidacaoTabela()
        {
            if (comboTabela.SelectedIndex == -1)
            {
                MessageBox.Show("Tabela não selecionada, Verifique!");
                return false;
            }

            return true;
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                DB.TpBanco tipoDB;
                if (Validacao(out tipoDB, true))
                {
                    if (ValidacaoTabela())
                    {
                        if (comboLinguagem.SelectedItem.ToString() == "C#")
                        {
                            Pragma.CSharp.GerarModel(txtPacote.Text, comboTabela.SelectedValue.ToString(), txtServidor.Text.Trim(), comboBanco.SelectedValue.ToString(), tipoDB, RetornaUsuario(), cbDataAnnotations.Checked);
                        }
                        else if (comboLinguagem.SelectedItem.ToString() == "Java")
                        {
                            Pragma.Java.GerarModel(txtPacote.Text, comboTabela.SelectedValue.ToString(), txtServidor.Text.Trim(), comboBanco.SelectedValue.ToString(), tipoDB, RetornaUsuario());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
