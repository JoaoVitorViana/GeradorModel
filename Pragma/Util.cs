using DB;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;

namespace Pragma
{
	public class Util
	{
		public static void GravaLog(Exception pException, string pPrograma)
		{
			try
			{
				SqlServer.GravarLog(pPrograma, pException);
			}
			catch (Exception ex)
			{
				GravarLog(ex.ToString(), pPrograma);
			}
		}

		public static void GravaLog(string pLog, string pPrograma)
		{
			try
			{
				SqlServer.GravarLog(pPrograma, new Exception(pLog));
			}
			catch (Exception ex)
			{
				GravarLog(ex.ToString(), pPrograma);
			}
		}

		public static void GravarLog(string pLog, string pPrograma)
		{
			try
			{
				GravaTexto(PathLog, pLog, pPrograma);
			}
			catch { }
		}

		public static string GravaTexto(string pPathLog, string pLog, string pNome, string pFormat = ".txt")
		{
			if (!Directory.Exists(pPathLog))
				Directory.CreateDirectory(pPathLog);

			string retorno = pPathLog + pNome + pFormat;

			StreamWriter sw = new StreamWriter(retorno, false, Encoding.Default);
			sw.Write(pLog);
			sw.Close();

			return retorno;
		}

		public static string PathLog = AppDomain.CurrentDomain.BaseDirectory + @"Log\";

		public static string PathBkp = @"D:\bkp";

		public static string GetUser()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("/**");
			sb.AppendLine($" * Created by {Environment.UserName} on {DateTime.Now.ToString("dd/MM/yyyy")}.");
			sb.AppendLine(" */");
			return sb.ToString();
		}

		public static bool EnviarEmail(string pTitulo, string pAssunto, string pMensagem)
		{
			try
			{
				Email email = new Email();
				Email.DadosEmail dadosEmail = new Email.DadosEmail();
				dadosEmail.Assunto = pAssunto;
				dadosEmail.Titulo = pTitulo;

				List<string> destinatarios = new List<string>();
				string[] arrayEmail = ConfigurationManager.AppSettings["enderecosEmail"].Split(';');
				if (arrayEmail != null)
					foreach (string item in arrayEmail)
						destinatarios.Add(item);

				dadosEmail.ListaDestinatarios = destinatarios;
				dadosEmail.Mensagem = pMensagem;
				dadosEmail.ListaAnexo = null;

				email.EnviaEmail(Email.TipoEmail.Erro, pTitulo, dadosEmail);
				return true;
			}
			catch (Exception ex)
			{
				GravaLog(ex, "Enviar_Email");
				return false;
			}
		}

		public static Tabela GetTabela(TpBanco pTpBanco, string pTabela, string pServidor, string pBanco, UserDB pUsuario, bool pQuery, string pComando)
		{
			Tabela tabela = null;
			var schemaTabela = pTabela.Split('.');
			string schema = string.Empty;
			string tabelaNome = pTabela;
			if (schemaTabela != null && schemaTabela.Length > 1)
			{
				schema = schemaTabela[0];
				tabelaNome = schemaTabela[1];
			}
			
			if (pQuery)
				tabela = GetQueryInfo(pComando, pServidor, tabelaNome, pTpBanco, pUsuario);
			else
				switch (pTpBanco)
				{
					case TpBanco.SqlServer:
						tabela = SqlServer.GetTabelaInfo(tabelaNome, pServidor, pBanco, pUsuario, schema);
						break;
					case TpBanco.MySql:
						tabela = MySql.GetTabelaInfo(tabelaNome, pServidor, pBanco, pUsuario);
						break;
					default:
						throw new NotImplementedException();
				}

			if (tabela == null)
				throw new NotImplementedException();

			return tabela;
		}

		public static Tabela GetQueryInfo(string pQuery, string pServidor, string pTabela, TpBanco pTpBanco, UserDB pUsuario)
		{
			if (pQuery == null)
				throw new Exception("Query não informada, Verifique");

			DataTable dt = new DataTable();
			switch (pTpBanco)
			{
				case TpBanco.SqlServer:
					dt = SqlServer.RetornaDB(pUsuario, pServidor).ExecuteDataTable(pQuery);
					break;
				case TpBanco.MySql:
					dt = new DB.MySql(pServidor, pUsuario.Usuario, pUsuario.Senha).ExecuteDataTable(pQuery);
					break;
			}

			Tabela tabela = new Tabela();
			tabela.Nome = pTabela.Trim();

			List<Campos> campos = new List<Campos>();
			Campos campo = null;
			foreach (DataColumn dc in dt.Columns)
			{
				campo = new Campos();
				campo.Chave = false;
				campo.Nome = dc.ColumnName;
				campo.NotNull = false;
				campo.Tipo = new Tipo(dc.DataType.ToString());
				campos.Add(campo);
			}

			tabela.Campos = campos;

			return tabela;
		}
	}
}