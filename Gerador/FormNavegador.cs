using DB;
using Microsoft.Win32;
using Pragma.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Utilidades
{
	public partial class FormNavegador : Form
    {
        protected string mensagem;
        protected string erro;
        protected bool chamadoAberto;
        private List<VendaSgv> lsVendas;
        private PassosSGV passos;
        public void setUrl(string pUrl)
        {
            if (string.IsNullOrWhiteSpace(pUrl))
            {
                wb.Stop();
                this.Close();
            }

            wb.Navigate(pUrl);

            if (pUrl.Equals("https://sgv.redetendencia.com.br/sgv/login/autenticacao.jsp"))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT DISTINCT *");
                    sb.AppendLine("FROM [dbo].[LOG_INTEGRACAO]");
                    sb.AppendLine("WHERE CONVERT(DATE,DATA) = CONVERT(DATE,GETDATE())");
                    sb.AppendLine("AND PROGRAMA = 'VendaSGV'");
                    sb.AppendLine("AND ERRO LIKE '%500%'");

                    DataTable dt = new SqlServer("SRVSQL07", "INTRAFLEX").ExecuteDataTable(sb.ToString());
                    lsVendas = new List<VendaSgv>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        string json = dr["ERRO_COMPLETO"].ToString();
                        json = json.Substring(json.IndexOf("Json: ") + 6);
                        VendaSgv venda = new Pragma.Requisicao().DeserializarObjeto<VendaSgv>(json);
                        lsVendas.Add(venda);
                    }
                }
                catch
                {
                    wb.Stop();
                    this.Close();
                }
            }
            else if (pUrl.Contains("autoservico.redetendencia"))
            {
                SqlServer dbConexao = new SqlServer("SRVSQL07", "INTRAFLEX");

                chamadoAberto = false;

                var appName = Process.GetCurrentProcess().ProcessName + ".exe";
                SetIE8KeyforWebBrowserControl(appName);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT ERRO_COMPLETO, ERRO ");
                sb.AppendLine("FROM [dbo].[LOG_INTEGRACAO]");
                sb.AppendLine("WHERE CONVERT(DATE,DATA,103) >= CONVERT(DATE,GETDATE(),103)");
                sb.AppendLine("AND PROGRAMA = 'VendaSGV'");
                sb.AppendLine("AND ERRO = 'The remote server returned an error: (500) Internal Server Error.'");
                sb.AppendLine("GROUP BY ERRO_COMPLETO, ERRO ");
                DataTable dt = dbConexao.ExecuteDataTable(sb.ToString());

                mensagem = string.Empty;
                if (DateTime.Now.Hour >= 00 && DateTime.Now.Hour < 13)
                    mensagem += "Bom dia! " + Environment.NewLine;
                else if (DateTime.Now.Hour >= 13 && DateTime.Now.Hour < 18)
                    mensagem += "Boa tarde! " + Environment.NewLine;
                else
                    mensagem += "Boa noite! " + Environment.NewLine;

                mensagem += "Estamos com retorno 500 ao injetar as seguintes vendas:" + Environment.NewLine;

                mensagem += Environment.NewLine;
                erro = string.Empty;

                foreach (DataRow dr in dt.Rows)
                {
                    sb = new StringBuilder();
                    sb.AppendLine("SELECT Id");
                    sb.AppendLine("FROM [REDEFLEX_MOBILE].[dbo].[ErroIntegracaoSGV]");
                    sb.AppendLine("WHERE IdVendaItem = @IdVendaItem");
                    sb.AppendLine("AND Finalizado = 0");

                    string codigoVenda = dr["ERRO_COMPLETO"].ToString().Substring(0, dr["ERRO_COMPLETO"].ToString().IndexOf(" <html>"));
                    codigoVenda = codigoVenda.Replace("IdVenda:", "").Replace(",", "").Trim();
                    List<DBParametros> pmts = new List<DBParametros>();
                    pmts.Add(new DBParametros { Value = codigoVenda, Name = "IdVendaItem" });

                    object obIdVenda = dbConexao.ExecuteScalar(sb.ToString(), pmts);

                    if (obIdVenda == null || obIdVenda == DBNull.Value)
                    {
                        sb = new StringBuilder();
                        sb.AppendLine("INSERT INTO [REDEFLEX_MOBILE].[dbo].[ErroIntegracaoSGV](IdVendaItem,Mensagem,DataHora,Finalizado)");
                        sb.AppendLine("VALUES (@IdVendaItem,@Mensagem,GETDATE(),0)");
                        pmts.Add(new DBParametros { Value = dr["ERRO_COMPLETO"].ToString(), Name = "Mensagem" });

                        dbConexao.ExecuteNonQuery(sb.ToString(), pmts);

                        mensagem += dr["ERRO_COMPLETO"].ToString().Substring(dr["ERRO_COMPLETO"].ToString().IndexOf("</html>") + 7).Trim() + Environment.NewLine;
                        erro = dr["ERRO_COMPLETO"].ToString().Substring(dr["ERRO_COMPLETO"].ToString().IndexOf(",") + 1);
                        erro = erro.Substring(0, erro.IndexOf("</html>") + 7).Trim();
                    }
                }

                if (!string.IsNullOrWhiteSpace(erro))
                {
                    mensagem += Environment.NewLine;
                    mensagem += "Dados do acesso" + Environment.NewLine;
                    mensagem += "Usuário: RedeFlex" + Environment.NewLine;
                    mensagem += "Senha: RF#02SAB" + Environment.NewLine;
                    mensagem += "Endereço: https://services.redetendencia.com.br/api-rest/ms-injetor/injetor/auth/registrar-transacao" + Environment.NewLine;

                    mensagem += Environment.NewLine;
                    mensagem += "Erro:" + Environment.NewLine;
                    mensagem += erro;
                }
                else
                {
                    wb.Stop();
                    this.Close();
                }
            }
        }
        public FormNavegador()
        {
            InitializeComponent();
            wb.ScriptErrorsSuppressed = true;
            wb.AllowNavigation = true;
        }
        private void SetIE8KeyforWebBrowserControl(string appName)
        {
            RegistryKey Regkey = null;
            try
            {
                if (Environment.Is64BitOperatingSystem)
                    Regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\MAIN\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                else
                    Regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

                if (Regkey == null)
                    throw new Exception("Falha na configuração do aplicativo");

                string FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                if (FindAppkey == "8000")
                {
                    Regkey.Close();
                    return;
                }

                if (string.IsNullOrEmpty(FindAppkey))
                    Regkey.SetValue(appName, unchecked((int)0x1F40), RegistryValueKind.DWord);

                FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                if (FindAppkey != "8000")
                    throw new Exception("Falha na configuração do aplicativo, Ref: " + FindAppkey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Regkey != null)
                    Regkey.Close();
            }
        }
        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                string usuario = ConfigurationManager.AppSettings["usuarioSGV"].ToString();
                string senha = ConfigurationManager.AppSettings["senhaSGV"].ToString();

                if (wb.Url.ToString().Contains("http://www.concursos.ufmt.br/Portal/"))
                {
                    timerEvento.Start();
                }
                else if (wb.Url.ToString().Equals("https://autoservico.redetendencia.com.br/"))
                {
                    wb.Document.GetElementById("User").SetAttribute("value", usuario);
                    wb.Document.GetElementById("Password").SetAttribute("value", senha);
                    wb.Document.GetElementsByTagName("button")[0].InvokeMember("click");
                }
                else if (wb.Url.ToString().Equals("https://autoservico.redetendencia.com.br/otrs/customer.pl?Action=CustomerTicketOverview;Subaction=MyTickets"))
                {
                    wb.Navigate("https://autoservico.redetendencia.com.br/otrs/customer.pl?Action=CustomerTicketMessage");
                }
                else if (wb.Url.ToString().Equals("https://autoservico.redetendencia.com.br/otrs/customer.pl?Action=CustomerTicketMessage"))
                {
                    timerEvento.Start();
                }
                else if (wb.Url.ToString().Equals("https://sgv.redetendencia.com.br/sgv/login/autenticacao.jsp"))
                {
                    wb.Document.GetElementById("usrNome").SetAttribute("value", usuario);
                    wb.Document.GetElementById("password").SetAttribute("value", senha);
                    wb.Document.GetElementsByTagName("input")[2].InvokeMember("click");
                }
                else if (wb.Url.ToString().Equals("https://sgv.redetendencia.com.br/sgv/paginas/home/index.jsf"))
                {
                    timerEvento.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " | " + ex.StackTrace);
            }
        }
        private void timerEvento_Tick(object sender, EventArgs e)
        {
            try
            {
                if (wb.Url.ToString().Contains("ufmt"))
                {
                    HtmlDocument document = wb.Document;
                    HtmlElementCollection arrays = document.GetElementsByTagName("table");
                }
                else if (wb.Url.ToString().Contains("autoservico.redetendencia"))
                {
                    if (chamadoAberto)
                    {
                        timerEvento.Stop();
                        wb.Stop();
                        this.Close();
                    }
                    else
                    {
                        if (wb.Document.GetElementById("ServiceID1").GetAttribute("value") == "-")
                        {
                            wb.Document.GetElementById("ServiceID1").Focus();
                            wb.Document.GetElementById("ServiceID1").SetAttribute("value", "Suporte a Produtos");
                            wb.Document.InvokeScript("eval", new object[] { "$('#ServiceID1').change();" });
                        }
                        else if (wb.Document.GetElementById("ServiceID2") != null && wb.Document.GetElementById("ServiceID2").GetAttribute("value") == "Suporte a Produtos::-")
                        {
                            wb.Document.GetElementById("ServiceID2").Focus();
                            wb.Document.GetElementById("ServiceID2").SetAttribute("value", "Suporte a Produtos::Integrações");
                            wb.Document.InvokeScript("eval", new object[] { "$('#ServiceID2').change();" });
                        }
                        else if (wb.Document.GetElementById("ServiceID3") != null && wb.Document.GetElementById("ServiceID3").GetAttribute("value") == "Suporte a Produtos::Integrações::-")
                        {
                            wb.Document.GetElementById("ServiceID3").Focus();
                            wb.Document.GetElementById("ServiceID3").SetAttribute("value", "Suporte a Produtos::Integrações::API Web Services");
                            wb.Document.InvokeScript("eval", new object[] { "$('#ServiceID3').change();" });
                        }
                        else if (wb.Document.GetElementById("ServiceID4") != null && wb.Document.GetElementById("ServiceID4").GetAttribute("value") == "Suporte a Produtos::Integrações::API Web Services::-")
                        {
                            wb.Document.GetElementById("ServiceID4").Focus();
                            wb.Document.GetElementById("ServiceID4").SetAttribute("value", "Suporte a Produtos::Integrações::API Web Services::Erro Sistemico");
                            wb.Document.InvokeScript("eval", new object[] { "$('#ServiceID4').change();" });
                        }
                        else if (wb.Document.GetElementById("Subject").GetAttribute("value") == "")
                            wb.Document.GetElementById("Subject").SetAttribute("value", "Erro no WebService de gravação de Vendas");
                        else
                        {
                            HtmlWindow iframe = wb.Document.Window.Frames[0];
                            if (iframe.Document.Body.InnerText == null || iframe.Document.Body.InnerText == "")
                                iframe.Document.Body.InnerText = mensagem;
                            else
                            {
                                wb.Document.GetElementById("submitRichText").InvokeMember("click");
                                chamadoAberto = true;
                            }
                        }
                    }
                }
                else
                {
                    if (lsVendas != null && lsVendas.Count > 0)
                    {
                        if (wb.Document.GetElementById("primeiroFocoPesquisa") == null)
                        {
                            var liMenu = wb.Document.Forms["menuForm"].GetElementsByTagName("li");
                            AbrirMenu(liMenu, "Estabelecimento");
                            passos = PassosSGV.SelecionarEmpresa;
                        }
                        else
                        {
                            switch (passos)
                            {
                                case PassosSGV.SelecionarEmpresa:
                                    if (wb.Document.GetElementById("empresaEstabelecimento").GetAttribute("value") != "")
                                        wb.Document.InvokeScript("eval", new object[] { "document.getElementById('empresaEstabelecimento').selectedIndex = 0" });
                                    passos = PassosSGV.SelecionarCliente;
                                    break;
                                case PassosSGV.SelecionarCliente:
                                    wb.Document.GetElementById("primeiroFocoPesquisa").SetAttribute("value", lsVendas[0].idCliente.ToString());
                                    wb.Document.GetElementById("botaoPesquisar").InvokeMember("click");
                                    passos = PassosSGV.AbrirModalCliente;
                                    break;
                                case PassosSGV.AbrirModalCliente:
                                    if (wb.Document.GetElementById("resultadoPesquisa_data").GetElementsByTagName("tr")[0].GetElementsByTagName("td").Count > 1)
                                    {
                                        wb.Document.GetElementById("resultadoPesquisa_data").GetElementsByTagName("tr")[0].GetElementsByTagName("td")[1].GetElementsByTagName("a")[1].InvokeMember("click");
                                        passos = PassosSGV.AbrirModalOperador;
                                    }
                                    else
                                        passos = PassosSGV.SelecionarCliente;
                                    break;
                                case PassosSGV.AbrirModalOperador:
                                    wb.Document.GetElementById("estabelecimentoTabView").GetElementsByTagName("a")[8].InvokeMember("click");
                                    passos = PassosSGV.CadastrarOperador;
                                    break;
                                case PassosSGV.CadastrarOperador:
                                    int rows = wb.Document.GetElementById("estabelecimentoTabView:operadors_data").GetElementsByTagName("tr")[0].GetElementsByTagName("td").Count;
                                    if (rows > 1)
                                    {
                                        lsVendas.RemoveAt(0);
                                        if (lsVendas.Count > 0)
                                        {
                                            wb.Document.GetElementById("estabelecimentoTabView:botaoCancelarOperador").InvokeMember("click");
                                            passos = PassosSGV.SelecionarCliente;
                                        }
                                        else
                                            passos = PassosSGV.Fechar;
                                    }
                                    else
                                    {
                                        wb.Document.GetElementById("estabelecimentoTabView:detalhesOperador:nomeOperador").SetAttribute("value", "redeflex");
                                        wb.Document.GetElementById("estabelecimentoTabView:detalhesOperador:senhaOperador").SetAttribute("value", "11020");
                                        wb.Document.GetElementById("estabelecimentoTabView:detalhesOperador:confirmacaoSenhaOperador").SetAttribute("value", "11020");
                                        wb.Document.GetElementById("estabelecimentoTabView:detalhesOperador").GetElementsByTagName("a")[1].InvokeMember("click");
                                        passos = PassosSGV.SelecionarAcesso;
                                    }
                                    break;
                                case PassosSGV.Fechar:
                                    timerEvento.Stop();
                                    wb.Stop();
                                    this.Close();
                                    break;
                                case PassosSGV.SelecionarAcesso:
                                    wb.Document.GetElementById("estabelecimentoTabView:detalhesOperador:papelTerminalEAAC").GetElementsByTagName("button")[0].InvokeMember("click");
                                    passos = PassosSGV.PesquisaPapel;
                                    break;
                                case PassosSGV.PesquisaPapel:
                                    wb.Document.GetElementById("papelPanelPesquisaCodigoPapelPesquisa").SetAttribute("value", "1");
                                    wb.Document.GetElementById("papelPesquisaDialogConsultaForm").GetElementsByTagName("button")[0].InvokeMember("click");
                                    passos = PassosSGV.SelecionarPapel;
                                    break;
                                case PassosSGV.SelecionarPapel:
                                    if (wb.Document.GetElementById("papelPesquisaDialogConsultaForm").GetElementsByTagName("tr")[2].GetElementsByTagName("td").Count > 1)
                                    {
                                        wb.Document.GetElementById("papelPesquisaDialogConsultaForm").GetElementsByTagName("tr")[2].GetElementsByTagName("td")[0].GetElementsByTagName("a")[0].InvokeMember("click");
                                        passos = PassosSGV.GravarPapel;
                                    }
                                    else
                                        passos = PassosSGV.PesquisaPapel;
                                    break;
                                case PassosSGV.GravarPapel:
                                    if (wb.Document.GetElementById("estabelecimentoTabView:detalhesOperador:pgGerenciarPapelOperador").GetElementsByTagName("tr")[1].GetElementsByTagName("td").Count > 1)
                                    {
                                        wb.Document.GetElementById("estabelecimentoTabView:botaoGravarOperador").InvokeMember("click");
                                        passos = PassosSGV.CadastrarOperador;
                                    }
                                    else
                                    {
                                        wb.Document.GetElementById("estabelecimentoTabView:detalhesOperador").GetElementsByTagName("a")[3].InvokeMember("click");
                                        passos = PassosSGV.Fechar;
                                    }
                                    break;

                            }
                        }
                    }
                    else
                    {
                        timerEvento.Stop();
                        wb.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " | " + ex.StackTrace);
            }
        }
        private void AbrirMenu(HtmlElementCollection Collection, string InnerText)
        {
            for (int i = 0; i < Collection.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(Collection[i].InnerText) && Collection[i].InnerText.Trim().Equals(InnerText))
                {
                    Collection[i].GetElementsByTagName("a")[0].InvokeMember("click");
                }
            }
        }
    }
}