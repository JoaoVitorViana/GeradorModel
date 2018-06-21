using DB;
using System.Text;

namespace Pragma
{
	public class CSharp
	{
		public static void GerarRestPost()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("        [ResponseType(typeof(Int32))]");

			Arquivos.Deletar();
			Arquivos.Gerar(sb.ToString(), "POST");
		}

		public static void GerarModel(string pNamespace, string pTabela, string pServidor, string pBanco, TpBanco pTpBanco, Model.UserDB pUsuario, bool pQuery = false, string pComando = null, bool pDataAnnotations = false)
		{
			Tabela tabela = Util.GetTabela(pTpBanco, pTabela, pServidor, pBanco, pUsuario, pQuery, pComando);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("using System;");
			if (pDataAnnotations)
			{
				sb.AppendLine("using System.ComponentModel.DataAnnotations;");
				sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
			}
			sb.AppendLine("");
			sb.AppendLine("namespace " + pNamespace);
			sb.AppendLine("{");
			if (pDataAnnotations)
				sb.AppendLine($"    [Table(\"{tabela.Nome}\", Schema = \"{tabela.Schema}\")]");
			sb.AppendLine($"    public partial class {tabela.Nome}");
			sb.AppendLine("    {");

			tabela.Campos.ForEach((campo) =>
			{
				if (pDataAnnotations)
				{
					if (campo.Chave)
						sb.AppendLine("        [Key]");
					else if (campo.NotNull)
						sb.AppendLine("        [Required(ErrorMessage = \"*Campo obrigatório.\")]");
					if (campo.Tipo.Banco.ToLower().Equals("date"))
						sb.AppendLine("        [DisplayFormat(DataFormatString = \"{0:dd/MM/yyyy}\", ApplyFormatInEditMode = true)]");
					if (campo.Tipo.Banco.ToLower().Equals("datetime"))
						sb.AppendLine("        [DisplayFormat(DataFormatString = \"{0:d}\", ApplyFormatInEditMode = true)]");
					else if (campo.Tipo.CSharp.ToLower().Equals("string") && campo.Tipo.Tamanho > 1 && campo.Tipo.Tamanho < 4000)
						sb.AppendLine($"        [StringLength({campo.Tipo.Tamanho}, ErrorMessage = \"Você atingiu o limite máximo de {campo.Tipo.Tamanho} caracteres permitidos.\")]");
				}
				sb.AppendLine($"        public {campo.Tipo.CSharp} {campo.Nome} " + "{ get; set; }");
			});

			sb.AppendLine("    }");
			sb.Append("}");

			Arquivos.Deletar();
			Arquivos.Gerar(sb.ToString(), "Model " + tabela.Nome);
		}

		public static void GerarRepository(string pNamespace, string pTabela, string pServidor, string pBanco, TpBanco pTpBanco, Model.UserDB pUsuario)
		{
			Tabela tabela = Util.GetTabela(pTpBanco, pTabela, pServidor, pBanco, pUsuario, false, null);

			string classeNome = $"{tabela.Nome}Repository";
			string interfaceNome = $"I{tabela.Nome}Repository";
			string model = "modelBuilder";

			Arquivos.Deletar();

			#region interface
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("using System;");
			sb.AppendLine("using System.Collections.Generic;");
			sb.AppendLine("using System.Linq;");
			sb.AppendLine("using System.Text;");
			sb.AppendLine("using System.Threading.Tasks;");
			sb.AppendLine("using ufmt.autenticacao.api.Models.Resources;");
			sb.AppendLine("");
			sb.AppendLine($"namespace {pNamespace}");
			sb.AppendLine("{");
			sb.AppendLine($"    public interface {interfaceNome}: IRepository<{classeNome}>");
			sb.AppendLine("    {");
			sb.AppendLine("        //TODO métodos");
			sb.AppendLine("    }");
			sb.Append("}");
			Arquivos.Gerar(sb.ToString(), "interface " + tabela.Nome);
			#endregion

			#region Repository
			sb = new StringBuilder();
			sb.AppendLine($"namespace {pNamespace}");
			sb.AppendLine("{");
			sb.AppendLine($"    public partial class {classeNome} : Repository<{classeNome}>, {interfaceNome}");
			sb.AppendLine("    {");
			sb.AppendLine("    }");
			sb.Append("}");
			Arquivos.Gerar(sb.ToString(), "Repository " + tabela.Nome);
			#endregion

			#region OnModelCreating
			sb = new StringBuilder();
			sb.AppendLine($"protected override void OnModelCreating(ModelBuilder {model})");
			sb.AppendLine("{");
			sb.AppendLine($"    {model}.Entity<{tabela.Nome}>(entity =>");
			sb.AppendLine("    {");
			tabela.Campos.ForEach((campo) =>
			{
				sb.AppendLine();
				if (campo.Chave)
				{
					sb.AppendLine($"       entity.HasKey(e => e.{campo.Nome});");
					sb.AppendLine();
				}
				sb.AppendLine($"       entity.Property(e => e.{campo.Nome})");
				if (campo.NotNull && !campo.Chave)
					sb.AppendLine("           .IsRequired()");
				if (campo.Tipo.CSharp.ToLower().Equals("string") && campo.Tipo.Tamanho > 1 && campo.Tipo.Tamanho < 4000)
					sb.AppendLine($"           .HasMaxLength({campo.Tipo.Tamanho})");
				if (campo.Tipo.Banco.ToLower().Contains("date"))
					sb.AppendLine($"           .HasColumnType(\"{campo.Tipo.Banco}\")");
				sb.AppendLine($"           .HasColumnName(\"{campo.Nome}\");");
			});
			sb.AppendLine("    }");
			sb.AppendLine("}");

			Arquivos.Gerar(sb.ToString(), $"OnModelCreating {tabela.Nome}");
			#endregion
		}
	}
}