using System;
using System.Windows.Forms;

namespace Utilidades
{
	public partial class FormPrincipal : Form
	{
		public FormPrincipal()
		{
			InitializeComponent();
			lbVersao.Text = "Versão: " + ProductVersion;
		}

		private void frm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Enabled = true;
		}

		private void androidToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AbrirForm(new FormAndroid());
		}

		private void dAOEModelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AbrirForm(new FormBanco());
		}

		private void AbrirForm(Form pForm)
		{
			pForm.FormClosing += new FormClosingEventHandler(frm_FormClosing);
			pForm.Show();
			Enabled = false;
		}

		private void btnTeste_Click(object sender, EventArgs e)
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}