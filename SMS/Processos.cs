using Model;
using Pragma;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace SMS
{
    public class Processos
    {
        public SmsEnvio EnviarSMS(string Numero, string Mensagem)
        {
            List<SmsEnvio> list = new List<SmsEnvio>();
            list.Add(new SmsEnvio
            {
                Numero = Numero,
                Mensagem = Mensagem
            });
            EnviarSMS(list);

            return list[0];
        }
        public void EnviarSMS(List<SmsEnvio> pSmsEnvio)
        {
            try
            {
                Conexao con = new Conexao();
                SerialPort port = null;

                string[] ports = SerialPort.GetPortNames();
                for (int i = 0; i < ports.Length; i++)
                {
                    try
                    {
                        port = con.OpenPort(ports[i]);
                    }
                    catch (Exception ex)
                    {
                        Pragma.Util.GravaLog(ex.Message, "SMS");
                    }

                    if (port != null)
                        break;
                }

                if (port != null)
                {
                    foreach (SmsEnvio numero in pSmsEnvio)
                    {
                        string recievedData = con.ExecCommand(port, "AT", 300, "Nenhum telefone conectado");
                        recievedData = con.ExecCommand(port, "AT+CMGF=1", 300, "Falha ao definir o formato da mensagem.");
                        string command = "AT+CMGS=\"+55" + numero.Numero + "\"";
                        recievedData = con.ExecCommand(port, command, 300, "Falha ao aceitar o número do telefone");
                        command = IO.RetiraCaracterEspecial(numero.Mensagem) + char.ConvertFromUtf32(26) + "\r";
                        recievedData = con.ExecCommand(port, command, 3000, "Falha ao enviar mensagem"); //3 seconds
                        
                        Thread.Sleep(4000);
                        Pragma.Util.GravaLog(recievedData, "SMS");

                        if (recievedData.EndsWith("\r\nOK\r\n"))
                        {
                            numero.DataHoraEnvio = DateTime.Now;
                            numero.Enviado = true;
                        }
                        else if (recievedData.Contains("ERROR"))
                        {
                            numero.DataHoraErro = DateTime.Now;
                            numero.Enviado = false;
                        }
                    }
                    con.ClosePort(port);
                }
            }
            catch (Exception ex)
            {
                Pragma.Util.GravaLog(ex.Message, "SMS");
                throw ex;
            }
        }
    }
}