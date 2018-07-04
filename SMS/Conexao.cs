using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace SMS
{
    public class Conexao
    {
        public AutoResetEvent receiveNow;

        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                {
                    receiveNow.Set();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SerialPort OpenPort(string p_strPortName)
        {
            receiveNow = new AutoResetEvent(false);
            SerialPort port = new SerialPort();

            try
            {
                port.PortName = p_strPortName;                 //COM1
                port.BaudRate = 9600;                          //9600
                port.DataBits = 8;                             //8
                port.StopBits = StopBits.One;                  //1
                port.Parity = Parity.None;                     //None
                port.ReadTimeout = 300;                        //300
                port.WriteTimeout = 300;                       //300
                port.Encoding = Encoding.GetEncoding("ISO-8859-1");
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Open();
                port.DtrEnable = true;
                port.RtsEnable = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return port;
        }

        public void ClosePort(SerialPort port)
        {
            try
            {
                port.Close();
                port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
                port = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ReadResponse(SerialPort port, int timeout)
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        if (buffer.Length > 0)
                            throw new ApplicationException("A resposta recebida é incompleta.");
                        else
                            throw new ApplicationException("Nenhum dado recebido do telefone.");
                    }
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer;
        }

        public string ExecCommand(SerialPort porta, string comandoATT, int tempoResposta, string mensagemErro)
        {
            try
            {
                porta.DiscardOutBuffer();
                porta.DiscardInBuffer();
                receiveNow.Reset();
                porta.Write(comandoATT + "\r");

                string input = ReadResponse(porta, tempoResposta);
                if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
                    throw new ApplicationException("Nenhuma mensagem de sucesso foi recebida.");
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}