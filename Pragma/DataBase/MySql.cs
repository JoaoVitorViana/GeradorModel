using Pragma.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Pragma.DataBase
{
	public class MySql
    {
        public static Tabela GetTabelaInfo(string pTabela, string pServidor, string pBanco, UserDB pUsuario)
        {
            Tabela tabela = new Tabela();
            tabela.Nome = pTabela.Trim();
            tabela.Banco = pBanco;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE, COLUMN_KEY, TABLE_SCHEMA");
            sb.AppendLine("FROM INFORMATION_SCHEMA.COLUMNS");
            sb.AppendLine("WHERE TABLE_NAME = '{1}'");
            sb.AppendLine("AND TABLE_SCHEMA = '{0}'");
            sb.AppendLine("ORDER BY ORDINAL_POSITION");

            DB.MySql dbConexao = new DB.MySql(pServidor, pUsuario.Usuario, pUsuario.Senha);
            DataTable dt = dbConexao.ExecuteDataTable(string.Format(sb.ToString(), tabela.Banco, tabela.Nome));

            List<Campos> campos = new List<Campos>();
            foreach (DataRow dr in dt.Rows)
            {
                Campos campo = new Campos();
                campo.Nome = dr[0].ToString();
                campo.NotNull = dr[3].ToString().ToUpper().Equals("NO") ? true : false;
                campo.Tipo = new TipoBanco(dr[1].ToString(), campo.NotNull);
                campo.Chave = dr[4].ToString().ToUpper().Equals("PRI") ? true : false;
                campos.Add(campo);
            }

            if (dt.Rows.Count > 0)
                tabela.Schema = dt.Rows[0][5].ToString();
            tabela.ExisteChave = campos.Where(i => i.Chave == true).Any();
            tabela.Campos = campos;
            return tabela;
        }

        public static DataTable GetBancos(string pServidor, UserDB pUsuario)
        {
            DB.MySql dbConexao = new DB.MySql(pServidor, pUsuario.Usuario, pUsuario.Senha);
            DataTable dt = dbConexao.ExecuteDataTable("SHOW DATABASES");
            dt.Columns[0].ColumnName = "Name";
            return dt;
        }

        public static DataTable GetTabelas(string pServidor, string pBanco, UserDB pUsuario)
        {
            DB.MySql dbConexao = new DB.MySql(pServidor, pUsuario.Usuario, pUsuario.Senha);
            DataTable dt = dbConexao.ExecuteDataTable($"SHOW TABLES FROM {pBanco}");
            dt.Columns[0].ColumnName = "Name";
            return dt;
        }
    }
}