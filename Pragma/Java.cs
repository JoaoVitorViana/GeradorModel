using DB;
using Pragma.Models;
using System.Linq;
using System.Text;

namespace Pragma
{
	public class Java
	{
		public static void GerarModel(string pPacote, string pTabela, string pServidor, string pBanco, TpBanco pTpBanco, UserDB pUsuario, bool pQuery = false, string pComando = null)
		{
			Tabela tabela = Util.GetTabela(pTpBanco, pTabela, pServidor, pBanco, pUsuario, pQuery, pComando);

			string nomeClasse = tabela.Nome;

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("package " + pPacote + ";");
			sb.AppendLine("");
			if (tabela.Campos.Where(c => c.Tipo.Java.Contains("Date")).Count() > 0)
			{
				sb.AppendLine("import java.util.Date;");
				sb.AppendLine("");
			}
			sb.Append(Util.GetUser());
			sb.AppendLine();
			sb.AppendLine($"public class {nomeClasse} " + "{");
			foreach (Campos campo in tabela.Campos)
				sb.AppendLine($"    private {campo.Tipo.Java} {campo.Nome}\";");

			foreach (Campos campo in tabela.Campos)
			{
				sb.AppendLine();
				sb.AppendLine($"    public {campo.Tipo.Java} get{campo.Nome}()" + " {");
				sb.AppendLine("        return this." + campo.Nome + ";");
				sb.AppendLine("    }");
				sb.AppendLine("");
				sb.AppendLine("    public void set" + campo.Nome + "(" + campo.Tipo.Java + " " + campo.Nome + ") {");
				sb.AppendLine("        this." + campo.Nome + " = " + campo.Nome + ";");
				sb.AppendLine("    }");
			}
			sb.Append("}");

			Arquivos.Deletar();
			Arquivos.Gerar(sb.ToString(), nomeClasse);
		}

		public static string GetTypeParametroJava(Campos campo, string pParametro, bool pVirgula = true)
		{
			return ((!campo.Tipo.Java.Equals("String")) ? "String.valueOf(" + pParametro + ")" : pParametro) + (pVirgula ? ", " : "");
		}

		public static string GetParametroJava(Campos campo) => $"p{IO.ToTitleCase(campo.Nome)}";

		public static string GetCampoJava(Campos campo, string pObjeto) => GetTypeParametroJava(campo, $"p{pObjeto}.get{IO.ToTitleCase(campo.Nome)}()", false);
	}
}