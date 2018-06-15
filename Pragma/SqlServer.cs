using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Pragma
{
	public class SqlServer
	{
		public static bool IsChave(string pTabela, string pServidor, string pBanco, string pColuna, UserDB pUsuario)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("SELECT KU.table_name AS TABLENAME, column_name AS PRIMARYKEYCOLUMN");
			sb.AppendLine($"FROM {pBanco}.INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC");
			sb.AppendLine($"JOIN {pBanco}.INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU ON (TC.CONSTRAINT_TYPE = 'PRIMARY KEY' AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME)");
			sb.AppendLine("WHERE KU.table_name = @Tabela");
			sb.AppendLine("AND KU.COLUMN_NAME = @Coluna");
			sb.AppendLine("ORDER BY KU.TABLE_NAME, KU.ORDINAL_POSITION");

			DB.SqlServer dbConexao = RetornaDB(pUsuario, pServidor);
			List<DB.DBParametros> pmts = new List<DB.DBParametros>();
			pmts.Add(new DB.DBParametros { Name = "Tabela", Value = pTabela });
			pmts.Add(new DB.DBParametros { Name = "Coluna", Value = pColuna });

			DataTable dtExiste = dbConexao.ExecuteDataTable(sb.ToString(), pmts);
			return dtExiste.Rows.Count > 0;
		}

		public static Tabela GetTabelaInfo(string pTabela, string pServidor, string pBanco, UserDB pUsuario)
		{
			Tabela tabela = new Tabela();
			tabela.Nome = pTabela.Trim();
			tabela.Banco = pBanco;

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, COLUMN_DEFAULT, CASE WHEN IS_NULLABLE = 'YES' THEN 0 ELSE 1 END NOTNULL, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH");
			sb.AppendLine($"FROM {tabela.Banco}.INFORMATION_SCHEMA.COLUMNS");
			sb.AppendLine("WHERE TABLE_NAME = @Tabela");
			sb.AppendLine("ORDER BY ORDINAL_POSITION");

			List<DB.DBParametros> pmts = new List<DB.DBParametros>();
			pmts.Add(new DB.DBParametros { Name = "Tabela", Value = tabela.Nome });

			DB.SqlServer dbConexao = RetornaDB(pUsuario, pServidor);
			DataTable dt = dbConexao.ExecuteDataTable(sb.ToString(), pmts);

			List<Campos> campos = new List<Campos>();
			foreach (DataRow dr in dt.Rows)
			{
				Campos campo = new Campos();
				campo.Nome = dr[3].ToString();
				campo.NotNull = Convert.ToBoolean(dr[5]);
				campo.Tipo = new Tipo(dr[6].ToString(), campo.NotNull, (dr[7] != DBNull.Value) ? dr[7].ToString() : null);
				campo.Chave = IsChave(pTabela, pServidor, pBanco, dr[3].ToString(), pUsuario);
				campos.Add(campo);
			}

			if (dt.Rows.Count > 0)
				tabela.Schema = dt.Rows[0][1].ToString();
			tabela.ExisteChave = campos.Where(i => i.Chave == true).Any();
			tabela.Campos = campos;
			return tabela;
		}

		public static DataTable GetBancos(string pServidor, UserDB pUsuario)
		{
			string query = "SELECT name AS Name FROM master.dbo.sysdatabases ORDER BY name";
			DB.SqlServer dbConexao = RetornaDB(pUsuario, pServidor);
			return dbConexao.ExecuteDataTable(query);
		}

		public static DB.SqlServer RetornaDB(UserDB pUsuario, string pServidor)
		{
			if (pUsuario == null)
				return new DB.SqlServer(pServidor);
			else
				return new DB.SqlServer(pServidor, "master", pUsuario.Usuario, pUsuario.Senha);
		}

		public static DataTable GetTabelas(string pServidor, string pBanco, UserDB pUsuario)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("SELECT TABLE_NAME AS Name");
			sb.AppendLine($"FROM {pBanco}.INFORMATION_SCHEMA.TABLES");
			sb.AppendLine("ORDER BY TABLE_NAME");
			DB.SqlServer dbConexao = RetornaDB(pUsuario, pServidor);
			return dbConexao.ExecuteDataTable(sb.ToString());
		}

		public static void GerarLog()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("SELECT c.session_id AS Sessao,");
			sb.AppendLine("c.net_transport,");
			sb.AppendLine("c.encrypt_option,");
			sb.AppendLine("c.auth_scheme,");
			sb.AppendLine("s.host_name AS Maquina,");
			sb.AppendLine("s.program_name AS Programa,");
			sb.AppendLine("s.client_interface_name AS Interface,");
			sb.AppendLine("s.login_name AS Usuario,");
			sb.AppendLine("s.nt_domain,");
			sb.AppendLine("s.nt_user_name,");
			sb.AppendLine("s.original_login_name,");
			sb.AppendLine("c.connect_time,");
			sb.AppendLine("s.login_time");
			sb.AppendLine("FROM sys.dm_exec_connections AS c");
			sb.AppendLine("JOIN sys.dm_exec_sessions AS s ON (c.session_id = s.session_id)");
			sb.AppendLine("WHERE c.session_id = @@SPID;");
		}

		public static void GerarBackupFull(string pLocalBkp, string pServidor, string pDataBase, UserDB pUsuario)
		{
			try
			{
				if (!File.Exists(pLocalBkp + @"\" + pDataBase + ".BAK"))
				{
					string query = "BACKUP DATABASE " + pDataBase + " TO DISK='" + pLocalBkp + @"\" + pDataBase + ".BAK'";
					RetornaDB(pUsuario, pServidor).ExecuteNonQuery(query);
					Util.GravaLog("Gerado BKP " + pDataBase + " com sucesso " + DateTime.Now.ToString("ddMMyyyy"), "GerarBackupFull");
				}
			}
			catch (Exception ex)
			{
				Util.GravaLog(ex, "GerarBackupFull");
				throw ex;
			}
		}

		public static void GerarBackupLog(string pLocalBkp, string pServidor, string pDataBase, UserDB pUsuario)
		{
			try
			{
				if (!File.Exists(pLocalBkp + @"\" + pDataBase + DateTime.Now.ToString("HHmmss") + ".TRN"))
				{
					string query = "BACKUP LOG " + pDataBase + " TO DISK='" + pLocalBkp + @"\" + pDataBase + DateTime.Now.ToString("HHmmss") + ".TRN'";
					RetornaDB(pUsuario, pServidor).ExecuteNonQuery(query);
					Util.GravaLog("Gerado BKP " + pDataBase + " com sucesso " + DateTime.Now.ToString("HHmmss"), "GerarBackupLog");
				}
			}
			catch (Exception ex)
			{
				Util.GravaLog(ex, "GerarBackupLog");
				throw ex;
			}
		}

		public static void GravarLog(string pProjeto, Exception pEx)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("IF OBJECT_ID('Viana.dbo.LogErro') IS NULL");
				sb.AppendLine("BEGIN");
				sb.AppendLine("	CREATE TABLE Viana.dbo.LogErro(Id BIGINT IDENTITY(1,1),");
				sb.AppendLine("						 CONSTRAINT pk_LogErro PRIMARY KEY (Id),");
				sb.AppendLine("						 Projeto VARCHAR(100),");
				sb.AppendLine("						 DataHora DATETIME CONSTRAINT df_LogErro_DataHora DEFAULT GETDATE(),");
				sb.AppendLine("						 Erro VARCHAR(1000),");
				sb.AppendLine("						 Erro_Completo VARCHAR(MAX))");
				sb.AppendLine("END");

				LogErro log = new LogErro();
				log.Projeto = pProjeto;
				log.Erro_Completo = pEx.ToString();
				log.Erro = pEx.Message;

				new DB.SqlServer(@"TI002833\TI002833").ExecuteNonQuery(sb.ToString());
				new DB.GenericSqlServer(@"TI002833\TI002833", "Viana").Salvar(log);
			}
			catch (Exception ex)
			{
				Util.GravarLog(pEx.ToString(), pProjeto);
				Util.GravarLog(ex.ToString(), pProjeto);
			}
		}

		public static void RotinaBackup(string pLocalBkp, string pServidor, string pDataBase)
		{
			try
			{
				string LocalFinal = pLocalBkp += @"\" + DateTime.Now.ToString("ddMMyyyy");
				Arquivos.CreateDirectory(LocalFinal);
				GerarBackupFull(pLocalBkp, pServidor, pDataBase, null);
				DB.SqlServer dbConexao = new DB.SqlServer(pServidor, pDataBase);
				object objData = dbConexao.ExecuteScalar("SELECT MAX(DataHora) FROM [dbo].[LogErro] WHERE Projeto = 'GerarBackupLog'");

				bool executou = false;
				if (objData != null && objData != DBNull.Value)
				{
					if (Convert.ToDateTime(objData).AddMinutes(15) < DateTime.Now)
						executou = false;
					else
						executou = true;
				}

				if (!executou)
					GerarBackupLog(pLocalBkp, pServidor, pDataBase, null);
			}
			catch (Exception ex)
			{
				Util.GravaLog(ex, "RotinaBackup");
			}
		}
	}
}