using System;
using System.Diagnostics;
using System.IO;

namespace Pragma
{
    public class Arquivos
    {
        public static void Gerar(string pArquivo, string pNome)
        {
            string arquivo = Util.GravaTexto(Util.PathLog, pArquivo, pNome);
            Process.Start(arquivo);
        }

        public static void Deletar()
        {
            if (Directory.Exists(Util.PathLog))
            {
                string[] arquivos = Directory.GetFiles(Util.PathLog);
                foreach (var item in arquivos)
                    File.Delete(item);
                Directory.Delete(Util.PathLog);
            }
        }

        public static void Deletar(string pLocal, int pQtdDias)
        {
            try
            {
                string[] arquivos = Directory.GetFiles(pLocal);
                foreach (var item in arquivos)
                {
                    FileInfo info = new FileInfo(item);
                    if (info.Extension.Equals(".db") && info.CreationTime < DateTime.Now.Date.AddDays(-pQtdDias))
                        File.Delete(item);
                }
            }
            catch { }
        }

        public static string[] GetFiles(string pLocalFinal)
        {
            string Local = string.Concat(pLocalFinal);
            CreateDirectory(Local);
            return Directory.GetFiles(Local);
        }

        public static void CreateDirectory(string pLocalFinal)
        {
            if (!Directory.Exists(pLocalFinal))
                Directory.CreateDirectory(pLocalFinal);
        }
    }
}