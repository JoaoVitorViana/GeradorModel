using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Utilidades
{
	public partial class FormBanco : Form
	{
		public FormBanco()
		{
			InitializeComponent();
			rbBanco.Checked = true;
			LiberaQuery(false);
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
							ComboTabela(Pragma.SqlServer.GetTabelas(txtServidor.Text.Trim(), comboBanco.SelectedValue.ToString(), RetornaUsuario()), true);
							break;
						case "My Sql":
							ComboTabela(Pragma.MySql.GetTabelas(txtServidor.Text.Trim(), comboBanco.SelectedValue.ToString(), RetornaUsuario()), true);
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

		private Model.UserDB RetornaUsuario()
		{
			Model.UserDB usuario = null;
			if (comboTipoLogon.SelectedIndex == 1)
			{
				usuario = new Model.UserDB();
				usuario.Usuario = txtUsuario.Text.Trim();
				usuario.Senha = txtSenha.Text.Trim();
			}
			return usuario;
		}

		private bool Validacao(out DB.TpBanco tipoDB, bool pValidarGeracao = false)
		{
			tipoDB = DB.TpBanco.Nenhum;
			if (!rbBanco.Checked && !rbQuery.Checked)
			{
				MessageBox.Show("Tipo da Geração não informada, Verifique!");
				return false;
			}

			if (comboDB.SelectedIndex == -1)
			{
				MessageBox.Show("Tipo DB não selecionado, Verifique!");
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

		private bool ValidacaoQuery()
		{
			if (string.IsNullOrWhiteSpace(txtQuery.Text))
			{
				MessageBox.Show("Query não informado, Verifique!");
				txtQuery.Focus();
				return false;
			}
			else if (!txtQuery.Text.ToUpper().StartsWith("SELECT"))
			{
				txtQuery.Focus();
				MessageBox.Show("Query inválida!");
				return false;
			}
			else if (txtQuery.Text.ToUpper().Contains("DELETE"))
			{
				txtQuery.Focus();
				MessageBox.Show("Query inválida!");
				return false;
			}
			else if (txtQuery.Text.ToUpper().Contains("UPDATE"))
			{
				txtQuery.Focus();
				MessageBox.Show("Query inválida!");
				return false;
			}
			else if (txtQuery.Text.ToUpper().Contains("INSERT"))
			{
				txtQuery.Focus();
				MessageBox.Show("Query inválida!");
				return false;
			}

			if (string.IsNullOrWhiteSpace(txtNomeClasse.Text))
			{
				MessageBox.Show("Nome da Classe não informada, Verifique!");
				txtNomeClasse.Focus();
				return false;
			}

			return true;
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

		private void btnGerarModel_Click(object sender, EventArgs e)
		{
			try
			{
				DB.TpBanco tipoDB;
				if (Validacao(out tipoDB, true))
				{
					if (rbBanco.Checked)
					{
						if (ValidacaoTabela())
						{
							switch (comboLinguagem.SelectedItem.ToString())
							{
								case "C#":
									Pragma.CSharp.GerarModel(txtPacote.Text, comboTabela.SelectedValue.ToString(), txtServidor.Text.Trim(), comboBanco.SelectedValue.ToString()
										, tipoDB, RetornaUsuario(), pDataAnnotations: cbDataAnnotations.Checked, pChaveEstrangeira: cbForeignKey.Checked);
									break;
								case "Java":
									Pragma.Java.GerarModel(txtPacote.Text, comboTabela.SelectedValue.ToString(), txtServidor.Text.Trim(), comboBanco.SelectedValue.ToString(), tipoDB, RetornaUsuario());
									break;
							}
						}
					}
					else
					{
						if (ValidacaoQuery())
						{
							switch (comboLinguagem.SelectedItem.ToString())
							{
								case "C#":
									Pragma.CSharp.GerarModel(txtPacote.Text, txtNomeClasse.Text.Trim(), txtServidor.Text.Trim(), null, tipoDB, RetornaUsuario(), true, txtQuery.Text.Trim());
									break;
								case "Java":
									Pragma.Java.GerarModel(txtPacote.Text, txtNomeClasse.Text.Trim(), txtServidor.Text.Trim(), null, tipoDB, RetornaUsuario(), true, txtQuery.Text.Trim());
									break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void rbBanco_CheckedChanged(object sender, EventArgs e)
		{
			LiberaQuery(false);
		}

		private void LiberaQuery(bool pLibera)
		{
			txtQuery.Enabled = pLibera;
			txtNomeClasse.Enabled = pLibera;
			int iTamanho = 0;
			if (pLibera)
				iTamanho = 1130;
			else
				iTamanho = 540;

			this.Size = new Size(iTamanho, 550);
			this.CenterToScreen();
			LimpaControles();
			if (rbBanco.Checked)
				rbQuery.Checked = false;
			if (rbQuery.Checked)
				rbBanco.Checked = false;
		}

		private void LimpaControles()
		{
			txtServidor.Text = "";
			ComboBanco(null, false);
			ComboTabela(null, false);
			txtQuery.Text = "";
		}

		private void rbQuery_CheckedChanged(object sender, EventArgs e)
		{
			LiberaQuery(true);
		}

		private void comboLinguagem_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (comboBox.SelectedIndex != -1)
			{
				switch (comboBox.SelectedItem.ToString())
				{
					case "C#":
						lbPacote.Text = "Namespace:";
						cbDataAnnotations.Checked = true;
						cbDataAnnotations.Enabled = true;
						cbAspNetCore.Enabled = true;
						cbForeignKey.Enabled = true;
						break;
					case "Java":
						lbPacote.Text = "Package:";
						cbDataAnnotations.Enabled = false;
						cbDataAnnotations.Checked = false;
						cbAspNetCore.Enabled = false;
						cbForeignKey.Enabled = false;
						break;
				}
			}

			cbAspNetCore.Checked = false;
			cbForeignKey.Checked = false;
			txtPacote.Text = "";
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

		private void btnDataBases_Click(object sender, EventArgs e)
		{
			try
			{
				DB.TpBanco tipoDB;
				if (Validacao(out tipoDB))
				{
					if (tipoDB == DB.TpBanco.SqlServer)
						ComboBanco(Pragma.SqlServer.GetBancos(txtServidor.Text.Trim(), RetornaUsuario()), true);
					else if (tipoDB == DB.TpBanco.MySql)
						ComboBanco(Pragma.MySql.GetBancos(txtServidor.Text.Trim(), RetornaUsuario()), true);
					else
						throw new NotImplementedException();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnGerarRepository_Click(object sender, EventArgs e)
		{
			try
			{
				DB.TpBanco tipoDB;
				if (Validacao(out tipoDB, true))
				{
					if (rbBanco.Checked)
					{
						if (ValidacaoTabela())
						{
							switch (comboLinguagem.SelectedItem.ToString())
							{
								case "C#":
									Pragma.CSharp.GerarRepositoryEntity(txtPacote.Text, comboTabela.Text, txtServidor.Text.Trim(), comboBanco.Text, tipoDB, RetornaUsuario(), cbAspNetCore.Checked, true, false, true);
									break;
								case "Java":
									throw new NotImplementedException();
							}
						}
					}
					else
						throw new NotImplementedException();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnGerarDBContext_Click(object sender, EventArgs e)
		{
			try
			{
				DB.TpBanco tipoDB;
				if (Validacao(out tipoDB, true))
				{
					if (rbBanco.Checked)
					{
						if (ValidacaoTabela())
						{
							switch (comboLinguagem.SelectedItem.ToString())
							{
								case "C#":
									Pragma.CSharp.GerarRepositoryEntity(txtPacote.Text, comboTabela.Text, txtServidor.Text.Trim(), comboBanco.Text, tipoDB, RetornaUsuario(), cbAspNetCore.Checked, false, true, false);
									break;
								case "Java":
									throw new NotImplementedException();
							}
						}
					}
					else
						throw new NotImplementedException();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}