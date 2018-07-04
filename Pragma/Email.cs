using Pragma.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Pragma
{
    public class Email
    {
        public enum TipoEmail
        {
            Alerta = 1,
            Erro = 2,
            Integracao = 3,
            ContatoPecaJa = 4,
            Chamados = 5,
            Boleto = 6
        }

        public class DadosEmail
        {
            public string Titulo { get; set; }
            public string Assunto { get; set; }
            public string Mensagem { get; set; }
            public List<String> ListaDestinatarios { get; set; }
            public List<String> ListaCopia { get; set; }
            public List<String> ListaCopiaOculta { get; set; }
            public List<Attachment> ListaAnexo { get; set; }
            public bool EnviaLogo { get; set; }
            public DadosEmail()
            {
                this.EnviaLogo = true;
            }
        }

        public Retorno EnviaEmail(TipoEmail Tipo, string Servico, DadosEmail DadosEmail)
        {
			Retorno ret = new Retorno();
            try
            {
                string SenderDisplayName = Servico;
                string Mensagem = "";

                ConfEmail config = new ConfEmail(Tipo);

                SmtpClient client = new SmtpClient();
                client.Host = config.Host;
                client.Port = Convert.ToInt32(config.Port);
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(config.UserName, config.PassWord);
                client.EnableSsl = config.EnableSsl;

                MailMessage message = new MailMessage();
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Sender = new MailAddress(config.SenderAddress, SenderDisplayName);
                message.From = new MailAddress(config.SenderAddress, SenderDisplayName);

                if (DadosEmail.ListaDestinatarios != null)
                {
                    foreach (String destinatario in DadosEmail.ListaDestinatarios)
                        message.To.Add(destinatario);
                }

                if (DadosEmail.ListaCopia != null)
                {
                    foreach (String copia in DadosEmail.ListaCopia)
                        message.CC.Add(copia);
                }

                if (DadosEmail.ListaCopiaOculta != null)
                {
                    foreach (String copiaOculta in DadosEmail.ListaCopiaOculta)
                        message.Bcc.Add(copiaOculta);
                }

                if (message.To.Count == 0)
                    message.To.Add(config.SenderAddress);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"font-size:13px; font-family:verdana, geneva, sans-serif\">");
                if (DadosEmail.EnviaLogo)
                {
                    try
                    {
                        Attachment inlineLogo = new Attachment(AppDomain.CurrentDomain.BaseDirectory + "redeflex.png");
                        inlineLogo.ContentId = "Image";
                        inlineLogo.ContentDisposition.Inline = true;
                        inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                        message.Attachments.Add(inlineLogo);
                        sb.AppendLine("        <tr>");
                        sb.AppendLine("            <td colspan=\"2\"><img id=\"redeFlex\" alt=\"redeFlex\" src=\"cid:Image\"></td>");
                        sb.AppendLine("        </tr>");
                    }
                    catch { }
                }

                if (DadosEmail.ListaAnexo != null && DadosEmail.ListaAnexo.Count > 0)
                {
                    foreach (var item in DadosEmail.ListaAnexo)
                        message.Attachments.Add(item);
                }

                if (!string.IsNullOrEmpty(DadosEmail.Titulo))
                {
                    sb.AppendLine("        <tr>");
                    sb.AppendLine("            <td colspan=\"2\"><strong>" + DadosEmail.Titulo + "</strong></td>");
                    sb.AppendLine("        </tr>");
                }
                sb.AppendLine("    </table>");
                sb.AppendLine("    <br />");

                if (DadosEmail.EnviaLogo || !string.IsNullOrEmpty(DadosEmail.Titulo))
                    Mensagem = sb.ToString();
                Mensagem += DadosEmail.Mensagem;
                Mensagem += "<br /><br /><br />";
                Mensagem += "<p>© " + DateTime.Now.Year.ToString() + " - Rede Flex</p>";

                message.Body = Mensagem;
                message.Subject = DadosEmail.Assunto;

                message.Priority = MailPriority.High;

                client.Send(message);
            }
            catch (Exception ex)
            {
                ret.Ok = false;
                ret.Erro = ex.Message;
            }
            return ret;
        }

        class ConfEmail
        {
            public string Host { get; }
            public string Port { get; }
            public string UserName { get; }
            public string PassWord { get; }
            public string SenderAddress { get; }
            public bool EnableSsl { get; }
            /// <summary>
            /// Tipo de Email a ser enviado ( Alerta, Erro ou Integração )
            /// </summary>
            /// <param name="pTipo">Alerta, Erro ou Integracao</param>
            public ConfEmail(TipoEmail pTipo)
            {
                if (pTipo == TipoEmail.Alerta)
                {
                    this.Host = "smtp.gmail.com";
                    this.Port = "587";
                    this.SenderAddress = "alerta.ti@redeflex.com.br";
                    this.UserName = "alerta.ti@redeflex.com.br";
                    this.PassWord = "f5gj#8Jy";
                    this.EnableSsl = true;
                }
                else if (pTipo == TipoEmail.Erro)
                {
                    this.Host = "smtp.gmail.com";
                    this.Port = "587";
                    this.SenderAddress = "alerta.ti@redeflex.com.br";
                    this.UserName = "alerta.ti@redeflex.com.br";
                    this.PassWord = "f5gj#8Jy";
                    this.EnableSsl = true;
                }
                else if (pTipo == TipoEmail.Integracao)
                {
                    this.Host = "smtp.gmail.com";
                    this.Port = "587";
                    this.SenderAddress = "integracao@redeflex.com.br";
                    this.UserName = "integracao@redeflex.com.br";
                    this.PassWord = "f5gj#8Jy";
                    this.EnableSsl = true;
                }
                else if (pTipo == TipoEmail.ContatoPecaJa)
                {
                    this.Host = "smtp.gmail.com";
                    this.Port = "587";
                    this.SenderAddress = "contato@redeflex.com.br";
                    this.UserName = "contato@redeflex.com.br";
                    this.PassWord = "f5gj#8Jy";
                    this.EnableSsl = true;
                }
                else if (pTipo == TipoEmail.Chamados)
                {
                    this.Host = "smtp.gmail.com";
                    this.Port = "587";
                    this.SenderAddress = "chamados@redeflex.com.br";
                    this.UserName = "chamados@redeflex.com.br";
                    this.PassWord = "sich@1606!";
                    this.EnableSsl = true;
                }
                else if (pTipo == TipoEmail.Boleto)
                {
                    this.Host = "smtp.gmail.com";
                    this.Port = "587";
                    this.SenderAddress = "boleto.mobile@redeflex.com.br";
                    this.UserName = "boleto.mobile@redeflex.com.br";
                    this.PassWord = "f5gj#8Jy";
                    this.EnableSsl = true;
                }
            }
        }
    }
}