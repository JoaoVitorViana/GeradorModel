using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace DB
{
    public class SqlLite
    {
        protected SQLiteConnection Conn;
        protected SQLiteCommand Cmd;
        protected SQLiteTransaction Tra = null;

        public SqlLite(string pDataBase)
        {
            try
            {
                Conn = new SQLiteConnection(string.Format(@"Data Source={0}", pDataBase));
                Cmd = new SQLiteCommand();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AbreConexao()
        {
            if (Conn.State != ConnectionState.Open)
                Conn.Open();
        }

        public void FechaConexao()
        {
            if (Conn.State == ConnectionState.Open)
                Conn.Close();
        }

        public DataTable ExecuteDataTable(string pComandoSQL, List<DBParametros> pParametros = null)
        {
            DataTable dt = new DataTable();
            AbreConexao();
            try
            {
                Cmd.Connection = Conn;
                Cmd.Parameters.Clear();
                Cmd.CommandText = pComandoSQL;
                Cmd.CommandType = CommandType.Text;

                if (pParametros != null)
                {
                    foreach (DBParametros item in pParametros)
                    {
                        Cmd.Parameters.AddWithValue(item.Name, item.Value == null ? DBNull.Value : item.Value);
                    }
                }

                SQLiteDataAdapter da = new SQLiteDataAdapter(Cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FechaConexao();
            }
            return dt;
        }

        public object ExecuteScalar(string pComandoSQL, List<DBParametros> pParametros = null)
        {
            object retorno;
            AbreConexao();
            try
            {
                Cmd.Connection = Conn;
                Cmd.Parameters.Clear();
                Cmd.CommandText = pComandoSQL;
                Cmd.CommandType = CommandType.Text;

                if (pParametros != null)
                {
                    foreach (DBParametros item in pParametros)
                    {
                        Cmd.Parameters.AddWithValue(item.Name, item.Value == null ? DBNull.Value : item.Value);
                    }
                }

                retorno = Cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FechaConexao();
            }

            return retorno;
        }

        public int ExecuteNonQuery(string pComandoSQL, List<DBParametros> pParametros = null)
        {
            int retorno;
            AbreConexao();
            try
            {
                Cmd.Connection = Conn;
                Cmd.Parameters.Clear();
                Cmd.CommandText = pComandoSQL;
                Cmd.CommandType = CommandType.Text;

                if (pParametros != null)
                {
                    foreach (DBParametros item in pParametros)
                    {
                        Cmd.Parameters.AddWithValue(item.Name, item.Value == null ? DBNull.Value : item.Value);
                    }
                }

                retorno = Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FechaConexao();
            }

            return retorno;
        }
    }
}