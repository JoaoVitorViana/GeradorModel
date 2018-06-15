using System;

namespace Utilidades.Models
{
	public class Despesas
	{
		public DateTime DataHora { get; set; }
		public DateTime DataPagamento { get; set; }
		public DateTime DataVencimento { get; set; }
		public string Descricao { get; set; }
		public bool Fixa { get; set; }
		public int Id { get; set; }
		public bool Pago { get; set; }
		public int Parcela { get; set; }
		public bool Repetir { get; set; }
		public int TotalParcelas { get; set; }
		public double Valor { get; set; }
	}
}
