using System;
using System.Collections.Generic;

namespace Pragma
{
    public class Chip
    {
        public class ChipInfo
        {
            public int Position { get; set; }
            public string ThisChar { get; set; }
            public Int64? Doubled { get; set; }
            public Int64? Summed { get; set; }
        }

        public static int ultimoDigito(string iccid)
        {
            List<ChipInfo> lista = new List<ChipInfo>();
            ChipInfo item;
            int retorno;
            char[] digito = iccid.ToCharArray();

            try
            {
                int pos = 0;
                foreach (char texto in digito)
                {
                    try
                    {
                        retorno = Convert.ToInt32(texto.ToString());
                        item = new ChipInfo();
                        item.Position = pos + 1;
                        item.ThisChar = retorno.ToString();
                        item.Doubled = null;
                        item.Summed = null;
                        lista.Add(item);
                        pos++;
                    }
                    catch
                    {
                        pos++;
                    }
                }

                bool positivo = false;
                if ((lista.Count % 2) == 0)
                    positivo = true;

                int idouble;
                for (int i = 0; i < lista.Count; i++)
                {
                    if (positivo)
                    {
                        if ((lista[i].Position % 2) == 0)
                        {
                            idouble = Convert.ToInt32(lista[i].ThisChar) * 2;
                            lista[i].Doubled = idouble;
                        }
                        else
                            continue;
                    }
                    else
                    {
                        if ((lista[i].Position % 2) == 0)
                            continue;
                        else
                        {
                            idouble = Convert.ToInt32(lista[i].ThisChar) * 2;
                            lista[i].Doubled = idouble;
                        }
                    }
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    Int64? doubled = lista[i].Doubled;
                    int resultado;
                    if (doubled == null)
                    {
                        resultado = Convert.ToInt32(lista[i].ThisChar);
                        lista[i].Summed = resultado;
                    }
                    else
                    {
                        if (doubled <= 9)
                            lista[i].Summed = doubled;
                        else
                        {
                            resultado = (Convert.ToInt32(doubled) / 10) + (Convert.ToInt32(doubled) - 10);
                            lista[i].Summed = resultado;
                        }
                    }
                }

                retorno = 0;

                foreach (ChipInfo chip in lista)
                    retorno += Convert.ToInt32(chip.Summed);

                int resto = retorno % 10;

                resto = 10 - resto;

                if (resto == 10)
                    resto = 0;

                return resto;
            }
            catch
            {
                return -1;
            }
        }
    }
}