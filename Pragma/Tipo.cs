using System;

namespace Pragma
{
	public class Tipo
	{
		public Tipo(string pTypeBanco, bool pNotNull, string pTamanho = null)
		{
			this.Banco = pTypeBanco;
			this.CSharp = pTypeBanco;
			this.Java = pTypeBanco;

			switch (pTypeBanco.ToLower())
			{
				case "bit":
					this.CSharp = "bool" + (pNotNull ? "" : "?");
					this.Java = "boolean";
					break;
				case "tinyint":
					this.CSharp = "byte" + (pNotNull ? "" : "?");
					break;
				case "binary":
				case "image":
				case "rowversion":
				case "timestamp":
				case "varbinary":
					this.CSharp = "byte[]";
					this.Java = "byte[]";
					break;
				case "date":
				case "datetime":
				case "datetime2":
				case "smalldatetime":
				case "datetimeoffset":
					this.CSharp = "DateTime" + (pNotNull ? "" : "?");
					this.Java = "Date";
					break;
				case "money":
				case "numeric":
				case "decimal":
				case "smallmoney":
					this.CSharp = "decimal" + (pNotNull ? "" : "?");
					this.Java = "double";
					break;
				case "float":
					this.CSharp = "double" + (pNotNull ? "" : "?");
					this.Java = "double";
					break;
				case "real":
					this.CSharp = "Single";
					this.Java = "float";
					break;
				case "smallint":
					this.CSharp = "Int16" + (pNotNull ? "" : "?");
					this.Java = "short";
					break;
				case "int":
				case "integer":
					this.CSharp = "int" + (pNotNull ? "" : "?");
					this.Java = "int";
					break;
				case "bigint":
					this.CSharp = "Int64" + (pNotNull ? "" : "?");
					this.Java = "java.math.BigInteger";
					break;
				case "char":
				case "nchar":
				case "ntext":
				case "nvarchar":
				case "text":
				case "varchar":
					this.CSharp = "string";
					this.Java = "String";
					if (pTypeBanco.Contains("(") && pTypeBanco.Contains(")"))
						this.Tamanho = Convert.ToInt32(pTypeBanco.Substring(pTypeBanco.IndexOf("(") + 1).Replace(")", ""));
					else if (!string.IsNullOrWhiteSpace(pTamanho))
						this.Tamanho = Convert.ToInt32(pTamanho);
					break;
				case "time":
					this.CSharp = "TimeSpan";
					this.Java = "java.sql.Time";
					break;
				case "xml":
					this.CSharp = "Xml";
					break;
			}
		}

		public Tipo(string pTypeData)
		{
			this.Banco = pTypeData.Replace("System.", "");
			this.CSharp = pTypeData.Replace("System.", "");
			this.Java = pTypeData.Replace("System.", "");

			if (pTypeData.ToUpper().Equals("BIGINT"))
			{
				this.CSharp = "Int64?";
				this.Java = "java.math.BigInteger";
				this.Banco = "BIGINT";
			}
			else if (pTypeData.ToUpper().Contains("STRING"))
			{
				this.CSharp = "string";
				this.Java = "String";
				this.Banco = "VARCHAR";
			}
			else if (pTypeData.ToUpper().Contains("INT32"))
			{
				this.CSharp = "int?";
				this.Java = "int";
				this.Banco = "int";
			}
			else if (pTypeData.ToUpper().Contains("DATETIME"))
			{
				this.CSharp = "DateTime?";
				this.Java = "Date";
				this.Banco = "DATETIME";
			}
			else if (pTypeData.ToUpper().Contains("BOOLEAN"))
			{
				this.CSharp = "bool?";
				this.Java = "boolean";
				this.Banco = "BIT";
			}
			else if (pTypeData.ToUpper().Contains("DECIMAL"))
			{
				this.CSharp = "decimal?";
				this.Java = "double";
				this.Banco = "Decimal";
			}
			else if (pTypeData.ToUpper().Contains("INT16"))
			{
				this.CSharp = "Int16?";
				this.Java = "int";
				this.Banco = "SMALLINT";
			}
		}

		public string Banco { get; set; }
		public string CSharp { get; set; }
		public string Java { get; set; }
		public int Tamanho { get; set; }
	}
}