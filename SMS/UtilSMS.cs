using Pragma;
using System.Text;

namespace SMS
{
    public class UtilSMS
    {
        /// <summary>
        /// Script de Criação das Tabelas para Enviar SMS
        /// </summary>
        /// <returns>Script</returns>
        public static void CriarTabela()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CREATE TABLE SmsEnvio (Id BIGINT IDENTITY(1,1),");
            sb.AppendLine("					   Numero VARCHAR(20),");
            sb.AppendLine("					   Mensagem VARCHAR(154),");
            sb.AppendLine("					   DataHora DATETIME CONSTRAINT dfSmsEnvioDataHora DEFAULT GETDATE(),");
            sb.AppendLine("					   DataHoraEnvio DATETIME,");
            sb.AppendLine("					   Enviado BIT CONSTRAINT dfSmsEnvioEnviado DEFAULT 0,");
            sb.AppendLine("					   DataHoraErro DATETIME,");
            sb.AppendLine("					   Erro VARCHAR(MAX),");
            sb.AppendLine("					   Tentativa INT CONSTRAINT dfSmsEnvioTentativa DEFAULT 0,");
            sb.AppendLine("					   CONSTRAINT pk_EnviaSms PRIMARY KEY (Id))");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), "Tabela SMS");
        }
    }
}