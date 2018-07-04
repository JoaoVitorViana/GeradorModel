using System.Collections.Generic;

namespace Pragma.Models
{
	public class Tabela
	{
		public string Banco { get; set; }
		public string Nome { get; set; }
		public string Schema { get; set; }
		public List<Campos> Campos { get; set; }
		public bool ExisteChave { get; set; }
		public string CamposChaves { get; set; }
		public string CamposValores { get; set; }
		public List<string> TabelasChaveEstrangeira { get; set; }
	}
}