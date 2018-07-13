namespace Pragma.Models
{
	public class Campos
	{
		public string Nome { get; set; }
		public TipoBanco Tipo { get; set; }
		public ChaveEstrangeira ChaveEstrangeira { get; set; }
		public bool Chave { get; set; }
		public bool NotNull { get; set; }
		public bool ValorDefault { get; set; }
		public bool Identity { get; set; }
	}
}