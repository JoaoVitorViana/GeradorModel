using System;

namespace Pragma.Models
{
    public class VendaSgv
    {
        public int idCliente { get; set; }
        public DateTime dataTransacao { get; set; }
        public int idProduto { get; set; }
        public int quantidade { get; set; }
        public double valorBruto { get; set; }
        public double valorLiquido { get; set; }
        public string tipoProdutoVendido { get; set; }
        public int NSUOrigem { get; set; }
        public int idTipoTransacao { get; set; }
    }
}