using System;
using System.Windows.Forms;

namespace Utilidades
{
    public partial class FormSMS : Form
    {
        public FormSMS()
        {
            InitializeComponent();
        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumero.Text))
                {
                    txtNumero.Focus();
                    MessageBox.Show("Número não informado, Verifique");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMensagem.Text))
                {
                    txtMensagem.Focus();
                    MessageBox.Show("Mensagem não informada, Verifique");
                    return;
                }

                SMS.Processos sms = new SMS.Processos();

                bool situacao = (bool)sms.EnviarSMS(txtNumero.Text.Trim(), txtMensagem.Text.Trim()).Enviado;
                MessageBox.Show((situacao) ? "Enviado" : "Não Enviado");
            }
            catch (Exception ex)
            {
                Pragma.Util.GravaLog(ex, "Utilidades");
                MessageBox.Show(ex.Message);
            }
        }
    }
}