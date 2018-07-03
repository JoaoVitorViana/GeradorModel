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
		public static Tabela GetTabelaInfo(string pTabela, string pServidor, string pBanco, UserDB pUsuario, string pSchema)
		{
			Tabela tabela = new Tabela
			{
				Nome = pTabela.Trim(),
				Banco = pBanco
			};

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, COLUMN_DEFAULT, CASE WHEN IS_NULLABLE = 'YES' THEN 0 ELSE 1 END AS NotNull, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH");
			sb.AppendLine(",(SELECT CASE WHEN COUNT(1) <= 0 THEN 0 ELSE 1 END");
			sb.AppendLine($"FROM [{tabela.Banco}].[INFORMATION_SCHEMA].[TABLE_CONSTRAINTS] AS TC");
			sb.AppendLine($"JOIN [{tabela.Banco}].[INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] AS KU ON (TC.CONSTRAINT_TYPE = 'PRIMARY KEY' AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME)");
			sb.AppendLine("WHERE KU.table_name = t0.TABLE_NAME");
			sb.AppendLine("AND KU.COLUMN_NAME = t0.COLUMN_NAME) AS IsChave");
			sb.AppendLine(",CAST(STUFF((SELECT  ',' + t.TargetTable");
			sb.AppendLine("FROM (");
			sb.AppendLine("SELECT ccu.table_name AS SourceTable");
			sb.AppendLine("    ,ccu.constraint_name AS SourceConstraint");
			sb.AppendLine("    ,ccu.column_name AS SourceColumn");
			sb.AppendLine("    ,kcu.table_name AS TargetTable");
			sb.AppendLine("    ,kcu.column_name AS TargetColumn");
			sb.AppendLine($"FROM [{tabela.Banco}].[INFORMATION_SCHEMA].[CONSTRAINT_COLUMN_USAGE] ccu");
			sb.AppendLine($"JOIN [{tabela.Banco}].[INFORMATION_SCHEMA].[REFERENTIAL_CONSTRAINTS] rc ON (ccu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME)");
			sb.AppendLine($"JOIN [{tabela.Banco}].[INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] kcu ON (kcu.CONSTRAINT_NAME = rc.UNIQUE_CONSTRAINT_NAME)");
			sb.AppendLine("WHERE ccu.TABLE_NAME = t0.TABLE_NAME");
			sb.AppendLine("AND ccu.TABLE_SCHEMA = t0.TABLE_SCHEMA");
			sb.AppendLine("AND ccu.COLUMN_NAME = t0.COLUMN_NAME");
			sb.AppendLine(") t FOR XML PATH('')), 1, 1, '') AS VARCHAR(MAX)) AS TabelaChaveEstrangeira");
			sb.AppendLine($"FROM [{tabela.Banco}].[INFORMATION_SCHEMA].[COLUMNS] t0");
			sb.AppendLine("WHERE t0.TABLE_NAME = @Tabela");
			if (!string.IsNullOrWhiteSpace(pSchema))
				sb.AppendLine("AND t0.TABLE_SCHEMA = @Schema");
			sb.AppendLine("ORDER BY ORDINAL_POSITION");

			List<DB.DBParametros> pmts = new List<DB.DBParametros>();
			pmts.Add(new DB.DBParametros { Name = "Tabela", Value = tabela.Nome });
			if (!string.IsNullOrWhiteSpace(pSchema))
				pmts.Add(new DB.DBParametros { Name = "Schema", Value = pSchema });

			DB.SqlServer dbConexao = RetornaDB(pUsuario, pServidor);
			DataTable dt = dbConexao.ExecuteDataTable(sb.ToString(), pmts);

			List<Campos> campos = new List<Campos>();
			foreach (DataRow dr in dt.Rows)
			{
				Campos campo = new Campos
				{
					Nome = dr[3].ToString(),
					NotNull = Convert.ToBoolean(dr[5]),
					Chave = Convert.ToBoolean(dr[8]),
					ChaveEstrangeira = new ChaveEstrangeira()
					{
						Is = dr[9] != DBNull.Value && !string.IsNullOrWhiteSpace(dr[9].ToString()),
						Tabelas = dr[9] != DBNull.Value && !string.IsNullOrWhiteSpace(dr[9].ToString()) ? dr[9].ToString().Split(',') : null
					}
				};
				campo.Tipo = new Tipo(dr[6].ToString(), campo.NotNull, (dr[7] != DBNull.Value) ? dr[7].ToString() : null);
				campos.Add(campo);
			}

			if (dt.Rows.Count > 0)
				tabela.Schema = dt.Rows[0][1].ToString();
			tabela.ExisteChave = campos.Where(i => i.Chave == true).Any();
			tabela.Campos = campos;

			//Chave Estrangeira
			sb = new StringBuilder();
			sb.AppendLine("SELECT ccu.table_name AS SourceTable");
			sb.AppendLine($"FROM [{tabela.Banco}].[INFORMATION_SCHEMA].[CONSTRAINT_COLUMN_USAGE] ccu");
			sb.AppendLine($"JOIN [{tabela.Banco}].[INFORMATION_SCHEMA].[REFERENTIAL_CONSTRAINTS] rc ON (ccu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME)");
			sb.AppendLine($"JOIN [{tabela.Banco}].[INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] kcu ON (kcu.CONSTRAINT_NAME = rc.UNIQUE_CONSTRAINT_NAME)");
			sb.AppendLine("WHERE 1=1");
			sb.AppendLine("AND kcu.TABLE_NAME = @Tabela");
			if (!string.IsNullOrWhiteSpace(pSchema))
				sb.AppendLine("AND kcu.TABLE_SCHEMA = @Schema");
			else if (!string.IsNullOrWhiteSpace(tabela.Schema))
			{
				sb.AppendLine("AND kcu.TABLE_SCHEMA = @Schema");
				pmts.Add(new DB.DBParametros { Name = "Schema", Value = tabela.Schema });
			}

			dt = dbConexao.ExecuteDataTable(sb.ToString(), pmts);
			List<string> ChaveEstrangeira = new List<string>();
			foreach (DataRow dr in dt.Rows)
				ChaveEstrangeira.Add(dr[0].ToString());
			tabela.TabelasChaveEstrangeira = ChaveEstrangeira;
			return tabela;
		}

		public static DataTable GetBancos(string pServidor, UserDB pUsuario)
		{
			string query = "SELECT name AS Name FROM [master].[dbo].[sysdatabases] ORDER BY name";
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
			sb.AppendLine("SELECT TABLE_SCHEMA + '.' + TABLE_NAME AS Name");
			sb.AppendLine($"FROM [{pBanco}].[INFORMATION_SCHEMA].[TABLES]");
			sb.AppendLine("ORDER BY 1");
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