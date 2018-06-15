using Pragma;
using System;
using System.Collections.Generic;
using System.Data;

namespace wsRotina
{
    public class Processamento
    {
        public static void Processar()
        {
            try
            {
                Arquivos.Deletar(@"\\Srviis\c$\bases_redeflex_mob", 7);

                DB.SqlServer dbViana = new DB.SqlServer(@"TI002833\TI002833", "Viana");
                DB.GenericSqlServer dbGenericViana = new DB.GenericSqlServer(@"TI002833\TI002833", "Viana");
                DB.SqlServer dbIntraflex = new DB.SqlServer("SRVSQL07", "INTRAFLEX");
                DB.SqlServer dbREDEFLEX_MOBILE = new DB.SqlServer(@"SRVDEV03\APLICACAO", "REDEFLEX_MOBILE");

                SqlServer.RotinaBackup(Util.PathBkp, @"TI002833\TI002833", "Viana");

				List<DB.Parametro> pmts = new List<DB.Parametro>
				{
					new DB.Parametro("Enviado", 0),
					new DB.Parametro("Tentativa", 6, DB.Operador.MenorIgual)
				};
				List<Model.SmsEnvio> listEnvio = dbGenericViana.GetItens<Model.SmsEnvio>(pmts, 5);
                if (listEnvio.Count > 0)
                {
                    try
                    {
                        SMS.Processos enviar = new SMS.Processos();
                        enviar.EnviarSMS(listEnvio);

                        listEnvio.ForEach((item) =>
                        {
                            dbGenericViana.Salvar(item);
                        });
                    }
                    catch (Exception er)
                    {
                        listEnvio.ForEach((item) =>
                        {
                            item.Enviado = false;
                            item.DataHoraErro = DateTime.Now;
                            item.Tentativa = item.Tentativa + 1;
                            item.Erro = er.Message;
                            dbGenericViana.Salvar(item);
                        });
                    }
                }

                object objCodigo = dbViana.ExecuteScalar("SELECT IdEnvio FROM [dbo].[ErroEnvio]");

                Int64 Codigo = 0;
                if (objCodigo != null && objCodigo != DBNull.Value)
                    Codigo = Convert.ToInt64(objCodigo);

                string query = @"SELECT TOP 2 Id, Erro
                                FROM [dbo].[LogErro]
                                WHERE CONVERT(DATE,DtHora) = CONVERT(DATE,GETDATE()) 
                                AND Erro NOT LIKE '%deadlocked%'
                                AND Atendido = 0
                                AND Id > {0}
                                ORDER BY Id";

                string[] numeros = new string[] { "65992237691", "65992782200", "65981426516", "65992737474", "65992403670" };
                DataTable dtErro = dbIntraflex.ExecuteDataTable(string.Format(query, Codigo.ToString()));

                foreach (DataRow item in dtErro.Rows)
                {
                    foreach (string numero in numeros)
                    {
                        Model.SmsEnvio envio = new Model.SmsEnvio();
                        string mensagem = "Log Erro: " + item[1].ToString();
                        if (mensagem.Length > 150)
                            mensagem = mensagem.Substring(150);
                        envio.Mensagem = mensagem;
                        envio.Numero = numero;
                        dbGenericViana.Salvar(envio);
                    }
                    Codigo = Convert.ToInt64(item[0]);
                }

				List<DB.DBParametros> pmts2 = new List<DB.DBParametros>
				{
					new DB.DBParametros { Name = "IdEnvio", Value = Codigo }
				};

				if (objCodigo == null || objCodigo == DBNull.Value)
                    dbViana.ExecuteNonQuery("INSERT INTO [dbo].[ErroEnvio](IdEnvio) VALUES (@IdEnvio)", pmts2);
                else
                    dbViana.ExecuteNonQuery("UPDATE [dbo].[ErroEnvio] SET IdEnvio = @IdEnvio", pmts2);

                object objData = dbViana.ExecuteScalar("SELECT MAX(DataHora) FROM [dbo].[LogErro] WHERE Projeto = 'Sp_Pendencias_Mobile'");
                bool executou = false;
                if (objData != null && objData != DBNull.Value)
                {
                    if (Convert.ToDateTime(objData).AddMinutes(10) < DateTime.Now)
                        executou = false;
                    else
                        executou = true;
                }

                if (!executou)
                {
                    dbREDEFLEX_MOBILE.ExecuteNonQuery("EXEC [dbo].[Sp_Pendencias_Mobile]");
                    Util.GravaLog("Executado", "Sp_Pendencias_Mobile");
                }

                objData = dbViana.ExecuteScalar("SELECT MAX(DataHora) FROM [dbo].[LogErro] WHERE Projeto = 'Sp_Preco_Dif'");
                if (objData != null && objData != DBNull.Value)
                {
                    if (Convert.ToDateTime(objData).AddMinutes(10) < DateTime.Now)
                        executou = false;
                    else
                        executou = true;
                }

                if (!executou)
                {
                    dbREDEFLEX_MOBILE.ExecuteNonQuery("EXEC [dbo].[Sp_Preco_Dif]");
                    Util.GravaLog("Executado", "Sp_Preco_Dif");
                }
            }
            catch (Exception ex)
            {
                Util.GravaLog(ex, "Processar");
            }
        }
    }
}