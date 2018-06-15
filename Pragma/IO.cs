using System.Globalization;

namespace Pragma
{
    public class IO
    {
        public static string ToTitleCase(string pTexto)
        {
            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
            return textInfo.ToTitleCase(pTexto);
        }

        public static string RetiraCaracterEspecial(string pTexto)
        {
            string letra;
            string resultado = "";

            for (int i = 0; i < pTexto.ToString().Length; i++)
            {
                letra = pTexto[i].ToString();
                switch (letra)
                {
                    case "á": letra = "a"; break;
                    case "é": letra = "e"; break;
                    case "í": letra = "i"; break;
                    case "ó": letra = "o"; break;
                    case "ú": letra = "u"; break;
                    case "à": letra = "a"; break;
                    case "è": letra = "e"; break;
                    case "ì": letra = "i"; break;
                    case "ò": letra = "o"; break;
                    case "ù": letra = "u"; break;
                    case "â": letra = "a"; break;
                    case "ê": letra = "e"; break;
                    case "î": letra = "i"; break;
                    case "ô": letra = "o"; break;
                    case "û": letra = "u"; break;
                    case "ä": letra = "a"; break;
                    case "ë": letra = "e"; break;
                    case "ï": letra = "i"; break;
                    case "ö": letra = "o"; break;
                    case "ü": letra = "u"; break;
                    case "ã": letra = "a"; break;
                    case "õ": letra = "o"; break;
                    case "ñ": letra = "n"; break;
                    case "ç": letra = "c"; break;
                    case "Á": letra = "A"; break;
                    case "É": letra = "E"; break;
                    case "Í": letra = "I"; break;
                    case "Ó": letra = "O"; break;
                    case "Ú": letra = "U"; break;
                    case "À": letra = "A"; break;
                    case "È": letra = "E"; break;
                    case "Ì": letra = "I"; break;
                    case "Ò": letra = "O"; break;
                    case "Ù": letra = "U"; break;
                    case "Â": letra = "A"; break;
                    case "Ê": letra = "E"; break;
                    case "Î": letra = "I"; break;
                    case "Ô": letra = "O"; break;
                    case "Û": letra = "U"; break;
                    case "Ä": letra = "A"; break;
                    case "Ë": letra = "E"; break;
                    case "Ï": letra = "I"; break;
                    case "Ö": letra = "O"; break;
                    case "Ü": letra = "U"; break;
                    case "Ã": letra = "A"; break;
                    case "Õ": letra = "O"; break;
                    case "Ñ": letra = "N"; break;
                    case "Ç": letra = "C"; break;
                    case "&": letra = ""; break;
                    case "º": letra = ""; break;
                    case "%": letra = ""; break;
                    case "@": letra = ""; break;
                    case "#": letra = ""; break;
                    case "¨": letra = ""; break;
                    case "*": letra = ""; break;
                    case "(": letra = ""; break;
                    case ")": letra = ""; break;
                    case "ª": letra = ""; break;
                    case "°": letra = ""; break;
                    case ";": letra = ""; break;
                    case "´": letra = ""; break;
                }
                resultado += letra;
            }

            return resultado;
        }
    }
}