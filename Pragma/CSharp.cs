﻿using DB;
using Pragma.Models;
using System.Linq;
using System.Text;

namespace Pragma
{
    public class CSharp
    {
        public static string GetSet = "{ get; set; }";

        public static void GerarRestPost()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("        [ResponseType(typeof(Int32))]");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), "POST");
        }

        public static void GerarModel(string pNamespace, string pTabela, string pServidor, string pBanco, TpBanco pTpBanco, UserDB pUsuario
                                    , bool pQuery = false, string pComando = null, bool pDataAnnotations = false, bool pChaveEstrangeira = false)
        {
            Tabela tabela = Util.GetTabela(pTpBanco, pTabela, pServidor, pBanco, pUsuario, pQuery, pComando);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            if (pDataAnnotations)
            {
                sb.AppendLine("using System.ComponentModel.DataAnnotations;");
                sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            }
            if (pChaveEstrangeira)
            {
                sb.AppendLine("using System.Collections.Generic;");
            }
            sb.AppendLine();
            sb.AppendLine("namespace " + pNamespace);
            sb.AppendLine("{");
            if (pDataAnnotations)
                sb.AppendLine($"    [Table(\"{tabela.Nome}\", Schema = \"{tabela.Schema}\")]");
            sb.AppendLine($"    public partial class {tabela.Nome}");
            sb.AppendLine("    {");

            StringBuilder sbForeignKey = null;
            if (pChaveEstrangeira)
            {
                if (tabela.TabelasChaveEstrangeira?.Count > 0)
                {
                    sb.AppendLine($"        public {tabela.Nome}()");
                    sb.AppendLine("        {");
                    tabela.TabelasChaveEstrangeira.ForEach((chave) =>
                    {
                        if (sbForeignKey == null)
                            sbForeignKey = new StringBuilder();

                        sb.AppendLine($"            {chave} = new HashSet<{chave}>();");
                        sbForeignKey.AppendLine($"        public virtual ICollection<{chave}> {chave} {GetSet}");
                    });
                    sb.AppendLine("        }");
                }
            }

            int qtdChaves = (tabela.Campos != null) ? tabela.Campos.Where(c => c.Chave).Count() : 0;
            int chaveIndex = 0;

            tabela.Campos?.ForEach((campo) =>
            {
                if (pDataAnnotations)
                {
                    if (campo.Chave)
                    {
                        sb.AppendLine("        [Key]");
                        if (qtdChaves > 1)
                        {
                            sb.AppendLine($"        [Column(Order = {chaveIndex++})]");
                            if (!campo.Identity) sb.AppendLine("        [DatabaseGenerated(DatabaseGeneratedOption.None)]");
                        }
                    }
                    else if (campo.Identity)
                        sb.AppendLine("        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                    else if (campo.NotNull)
                        sb.AppendLine("        [Required(ErrorMessage = \"*Campo obrigatório.\")]");
                    if (campo.Tipo.Banco.ToLower().Equals("date"))
                        sb.AppendLine("        [DisplayFormat(DataFormatString = \"{0:dd/MM/yyyy}\", ApplyFormatInEditMode = true)]");
                    if (campo.Tipo.Banco.ToLower().Equals("datetime"))
                        sb.AppendLine("        [DisplayFormat(DataFormatString = \"{0:d}\", ApplyFormatInEditMode = true)]");
                    else if (campo.Tipo.CSharp.ToLower().Equals("string") && campo.Tipo.Tamanho > 1 && campo.Tipo.Tamanho < 4000)
                        sb.AppendLine($"        [StringLength({campo.Tipo.Tamanho}, ErrorMessage = \"Você atingiu o limite máximo de {campo.Tipo.Tamanho} caracteres permitidos.\")]");
                }
                if (pChaveEstrangeira && campo.ChaveEstrangeira != null && campo.ChaveEstrangeira.Is)
                {
                    campo.ChaveEstrangeira.Tabelas?.ToList().ForEach((estrageira) =>
                    {
                        if (sbForeignKey == null)
                            sbForeignKey = new StringBuilder();

                        sbForeignKey.AppendLine($"        public virtual {estrageira} {estrageira} {GetSet}");
                        if (pDataAnnotations)
                            sb.AppendLine($"        [ForeignKey(\"{estrageira}\")]");
                    });
                }
                sb.AppendLine($"        public {campo.Tipo.CSharp} {campo.Nome} {GetSet}");
            });

            if (sbForeignKey != null)
                sb.Append(sbForeignKey);

            sb.AppendLine("    }");
            sb.Append("}");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), "Model " + tabela.Nome);
        }

        public static void GerarRepositoryEntity(string pNamespace, string pTabela, string pServidor, string pBanco, TpBanco pTpBanco, UserDB pUsuario
                                                , bool pAspNetCore = false, bool pGerarInterface = false, bool pGerarDBContext = false, bool pGerarRepository = false, string pQuery = null)
        {
            Tabela tabela = Util.GetTabela(pTpBanco, pTabela, pServidor, pBanco, pUsuario, pQuery != null, pQuery);

            string classeNome = $"{tabela.Nome}Repository";
            string interfaceNome = $"I{tabela.Nome}Repository";
            string model = "modelBuilder";
            string classeDB = "dbContext";

            Arquivos.Deletar();
            StringBuilder sb;

            #region interface
            if (pGerarInterface)
            {
                sb = new StringBuilder();
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
            }
            #endregion

            #region Repository
            if (pGerarRepository)
            {
                sb = new StringBuilder();
                sb.AppendLine($"namespace {pNamespace}");
                sb.AppendLine("{");
                sb.AppendLine($"    public partial class {classeNome} : Repository<{classeNome}>, {interfaceNome}");
                sb.AppendLine("    {");
                sb.AppendLine("    }");
                sb.Append("}");
                Arquivos.Gerar(sb.ToString(), "Repository " + tabela.Nome);
            }
            #endregion

            #region DbContext
            if (pGerarDBContext)
            {
                sb = new StringBuilder();
                if (pAspNetCore)
                    sb.AppendLine("using Microsoft.EntityFrameworkCore;");
                else
                    sb.AppendLine("using System.Data.Entity;");
                sb.AppendLine("");
                sb.AppendLine($"namespace {pNamespace}");
                sb.AppendLine("{");
                sb.AppendLine($"	public partial class {classeDB} : DbContext");
                sb.AppendLine("	{");
                if (pAspNetCore)
                {
                    sb.AppendLine($"		public {classeDB}()");
                    sb.AppendLine("		{");
                    sb.AppendLine("");
                    sb.AppendLine("		}");
                    sb.AppendLine("");
                    sb.AppendLine($"		public {classeDB}(DbContextOptions<{classeDB}> options) : base(options)");
                    sb.AppendLine("		{");
                    sb.AppendLine("");
                    sb.AppendLine("		}");
                }
                else
                {
                    sb.AppendLine($"		public {classeDB}() : base(\"name={classeDB}\")");
                    sb.AppendLine("		{");
                    sb.AppendLine("");
                    sb.AppendLine("		}");
                }
                sb.AppendLine("");
                sb.AppendLine($"		public virtual DbSet<{tabela.Nome}> {tabela.Nome} {GetSet}");
                sb.AppendLine("		");
                sb.AppendLine($"		protected override void OnModelCreating({((pAspNetCore) ? "ModelBuilder" : "DbModelBuilder")} {model})");
                sb.AppendLine("		{");
                if (pAspNetCore)
                {
                    sb.AppendLine($"			{model}.Entity<{tabela.Nome}>(entity =>");
                    sb.AppendLine("			{");
                    for (int i = 0; i < tabela.Campos.Count; i++)
                    {
                        if (i > 0) sb.AppendLine();
                        if (tabela.Campos[i].Chave)
                        {
                            sb.AppendLine($"				entity.HasKey(e => e.{tabela.Campos[i].Nome});");
                            sb.AppendLine();
                        }
                        sb.AppendLine($"				entity.Property(e => e.{tabela.Campos[i].Nome})");
                        if (tabela.Campos[i].NotNull && !tabela.Campos[i].Chave)
                            sb.AppendLine("					.IsRequired()");
                        if (tabela.Campos[i].Tipo.CSharp.ToLower().Equals("string") && tabela.Campos[i].Tipo.Tamanho > 1 && tabela.Campos[i].Tipo.Tamanho < 4000)
                            sb.AppendLine($"					.HasMaxLength({tabela.Campos[i].Tipo.Tamanho})");
                        if (tabela.Campos[i].Tipo.Banco.ToLower().Contains("date"))
                            sb.AppendLine($"					.HasColumnType({tabela.Campos[i].Tipo.Banco})");
                        sb.AppendLine($"					.HasColumnName(\"{tabela.Campos[i].Nome}\");");
                    }
                    if (!tabela.ExisteChave && tabela.Campos.Count > 0)
                    {
                        sb.AppendLine();
                        sb.AppendLine($"				entity.HasKey(e => e.{tabela.Campos[0].Nome});");
                    }
                    sb.AppendLine("			});");
                }
                else
                {
                    for (int i = 0; i < tabela.Campos.Count; i++)
                    {
                        if (i > 0) sb.AppendLine();
                        if (tabela.Campos[i].Chave)
                        {
                            sb.AppendLine($"			{model}.Entity<{tabela.Nome}>().HasKey<{tabela.Campos[i].Tipo.CSharp}>(s => s.{tabela.Campos[i].Nome});");
                            sb.AppendLine();
                        }
                        sb.AppendLine($"			{model}.Entity<{tabela.Nome}>()");
                        sb.AppendLine($"				.Property(p => p.{tabela.Campos[i].Nome})");
                        if (tabela.Campos[i].Tipo.CSharp.ToLower().Equals("string") && tabela.Campos[i].Tipo.Tamanho > 1 && tabela.Campos[i].Tipo.Tamanho < 4000)
                            sb.AppendLine($"				.HasMaxLength({tabela.Campos[i].Tipo.Tamanho})");
                        if (!tabela.Campos[i].Chave)
                        {
                            if (tabela.Campos[i].NotNull)
                                sb.AppendLine("				.IsRequired()");
                            else
                                sb.AppendLine("				.IsOptional()");
                        }
                        if (tabela.Campos[i].Tipo.Banco.ToLower().Contains("date"))
                            sb.AppendLine($"				.HasColumnType(\"{tabela.Campos[i].Tipo.Banco}\")");
                        sb.AppendLine($"				.HasColumnName(\"{tabela.Campos[i].Nome}\");");
                    }

                    tabela.TabelasChaveEstrangeira?.ForEach((chaves) =>
                    {
                        sb.AppendLine();
                        sb.AppendLine($"			{model}.Entity<{tabela.Nome}>()");
                        sb.AppendLine($"				.HasMany(e => e.{chaves})");
                        sb.AppendLine($"				.WithRequired(e => e.{tabela.Nome})");
                        sb.AppendLine("				.WillCascadeOnDelete(false);");
                    });

                    sb.AppendLine();
                    sb.AppendLine($"			{model}.Entity<{tabela.Nome}>().ToTable(\"{tabela.Nome}\");");
                }
                sb.AppendLine("		}");
                sb.AppendLine("");
                sb.AppendLine("	}");
                sb.Append("}");

                Arquivos.Gerar(sb.ToString(), $"OnModelCreating {tabela.Nome}");
            }
            #endregion
        }
    }
}